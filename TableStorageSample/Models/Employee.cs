using Azure;
using Azure.Data.Tables;

namespace TableStorageSample.Models;

public class Employee : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
}
