namespace FruitBasket.Core.Features.AddBanana;

public record AddBananaDto(DateOnly PurchaseDate, bool IsOrganic)
{
    public AddBananaRequest ToRequest() => new(PurchaseDate, IsOrganic);
}