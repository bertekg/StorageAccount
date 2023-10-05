using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using TableStorageSample.Models;

namespace TableStorageSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TableStorageController : ControllerBase
{
    TableClient tableClient;
    public TableStorageController()
    {
        string connectionString = "Tutaj dodaj connection string";
        TableServiceClient tableServiceClient = new(connectionString);

        tableClient = tableServiceClient.GetTableClient("employees");
    }
    [HttpPost]
    public async Task<IActionResult> Post(Employee employee)
    {
        await tableClient.CreateIfNotExistsAsync();

        await tableClient.AddEntityAsync(employee);

        return Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]string rowKey, [FromQuery]string partitionKey)
    {
        Response<Employee>? employee = await tableClient.GetEntityAsync<Employee>(partitionKey, rowKey);

        return Ok(employee);
    }

    [HttpGet("query")]
    public IActionResult Query()
    {
        Pageable<Employee>? employees = tableClient.Query<Employee>(e => e.PartitionKey == "IT");

        return Ok(employees);
    }
}
