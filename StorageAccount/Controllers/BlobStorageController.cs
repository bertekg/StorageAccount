using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

namespace StorageAccount.Controllers;

[ApiController]
[Route("[controller]")]
public class BlobStorageController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var connectionString = "Tutaj Connction String";
        BlobServiceClient blobServiceClient = new(connectionString);

        var containerName = "documents";

        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await blobContainerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

        //await blobClient.UploadAsync(file.OpenReadStream()); // bez nadpisywania
        //await blobClient.UploadAsync(file.OpenReadStream(), overwrite: true); // z nadpisywaniem

        BlobHttpHeaders blobHttpHeaders = new();
        blobHttpHeaders.ContentType = file.ContentType;
        await blobClient.UploadAsync(file.OpenReadStream(), blobHttpHeaders);

        return Ok();
    }
    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery]string blobName)
    {
        var connectionString = "Tutaj Connction String";
        BlobServiceClient blobServiceClient = new(connectionString);

        var containerName = "documents";

        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await blobContainerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

        Response<BlobDownloadResult>? downloadResponse = await blobClient.DownloadContentAsync();
        Stream? content = downloadResponse.Value.Content.ToStream();
        string? contentType = blobClient.GetProperties().Value.ContentType;

        return File(content, contentType, fileDownloadName: blobName);
    }
}
