namespace FruitBasket.Core.Interfaces;

public static class PartitionKeyHelper
{
    public static string CreateFuitsPartitionKey() => "fruits" + DateTime.UtcNow.ToString("yyyyMM");
}