using Azure;
using Azure.Data.Tables;

namespace FruitBasket.Core.Interfaces;

public class BaseEntity : ITableEntity
{
    public string? PartitionKey { get; set; } = PartitionKeyHelper.CreateFuitsPartitionKey();
    public string? RowKey { get; set; } = Guid.NewGuid().ToString();

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}