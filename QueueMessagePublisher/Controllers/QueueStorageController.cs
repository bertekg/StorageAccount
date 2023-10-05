using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using QueueMessagePublisher.Models;
using System.Text.Json;

namespace QueueMessagePublisher.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueStorageController : ControllerBase
{
    [HttpPost("publish")]
    public async Task<IActionResult> Publishi(ReturnDto returnDto)
    {
        string connectionString = "UseDevelopmentStorage=true";
        string queueName = "returns";

        QueueClient queueClient = new(connectionString, queueName);

        await queueClient.CreateIfNotExistsAsync();

        string serializedMessage = JsonSerializer.Serialize(returnDto);

        await queueClient.SendMessageAsync(serializedMessage);

        return Ok();
    }
}
