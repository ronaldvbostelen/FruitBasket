namespace FruitBasket.Infrastructure.Options;

public class TableStorageOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
}