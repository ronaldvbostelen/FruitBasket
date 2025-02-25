namespace FruitBasket.Core.Features.AddApple;

public record AddAppleDto(DateOnly PurchaseDate, AppleType Type)
{
    public AddAppleRequest ToRequest() => new(PurchaseDate, Type);
};