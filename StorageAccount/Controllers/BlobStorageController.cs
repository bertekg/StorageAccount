using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace StorageAccount.Controllers;

[ApiController]
[Route("[controller]")]
public class BlobStorageController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var connctionString = "Tutaj Connction String";
        BlobServiceClient blobServiceClient = new BlobServiceClient(connctionString);

        var containerName = "documents";

        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);


    }
}
