using FruitBasket.Core.Features.AddApple;

namespace FruitBasket.Core.Features.GetFruit;

public record FruitDto(string FruitType, DateTime PurchaseDate)
{
    public bool IsSpoiled { get; set; }
    public bool? IsOrganic { get; set; }
    public string? AppleType { get; set; }

    public static FruitDto Empty => new("Fruit:Unknown", DateTime.MinValue);
}