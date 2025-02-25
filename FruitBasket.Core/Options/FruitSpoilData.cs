namespace FruitBasket.Core.Options;

public class FruitSpoilData
{
    public int DefaultSpoilDays { get; set; }    
    public FruitSpoilDays[] FruitSpoilDays { get; set; } = [];
}

public record FruitSpoilDays(string Fruit, int BestBeforeDays);