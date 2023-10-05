using Azure.Storage.Queues;
using QueueMessagePublisher.Models;

string connectionString = "UseDevelopmentStorage=true";
string queueName = "returns";

QueueClient queueClient = new(connectionString, queueName);

while (true)
{
    var message = queueClient.ReceiveMessage();
    if (message.Value != null)
    {
        var dto = message.Value.Body.ToObjectFromJson<ReturnDto>();
        Processor(dto);
        await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
    }

    await Task.Delay(1000);
}

void Processor(ReturnDto dto)
{
    Console.WriteLine($"Processing return with id: {dto.Id}, " +
        $"for user: {dto.User}, " +
        $"from address: {dto.Address}");
}
