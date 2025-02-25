using FruitBasket.Core.Extensions;
using FruitBasket.Core.Features.AddApple;
using FruitBasket.Core.Features.AddBanana;
using FruitBasket.Core.Features.AddFruit;
using FruitBasket.Core.Features.GetFruit;

namespace FruitBasket.Core.Interfaces;

public static class EntityMapper
{
    public static AppleEntity MapToEntity(AddAppleRequest request) => new()
    {
        PurchaseDate = request.PurchaseDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime(),
        Type = request.Type,
    };

    public static BananaEntity MapToEntity(AddBananaRequest request) => new()
    {
        PurchaseDate = request.PurchaseDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime(),
        IsOrganic = request.IsOrganic,
    };

    public static FruitEntity MapToEntity(AddFruitRequest request) => new()
    {
        FruitType = request.FruitType.CapitalizeFirstLetter(),
        PurchaseDate = request.PurchaseDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime(),
    };

    public static FruitDto MapFromBanana(BananaEntity banana) =>
        new(banana.FruitType, banana.PurchaseDate)
        {
            IsOrganic = banana.IsOrganic
        };
    
    public static FruitDto MapFromApple(AppleEntity apple) =>
    new(apple.FruitType, apple.PurchaseDate)
    {
        AppleType = apple.Type.ToString()
    };

    public static FruitDto MapFromFruit(FruitEntity fruit) =>
        new(fruit.FruitType, fruit.PurchaseDate);
}