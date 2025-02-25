namespace FruitBasket.Core.Features.AddFruit;

public record AddFruitDto(DateOnly PurchaseDate)
{
    public AddFruitRequest ToRequest(string fruitType) => new(fruitType, PurchaseDate);
}