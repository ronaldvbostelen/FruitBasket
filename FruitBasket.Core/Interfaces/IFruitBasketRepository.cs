namespace FruitBasket.Core.Interfaces;

public interface IFruitBasketRepository
{
    List<FruitEntity> List();
    Task DeleteAllFruitAsync();
    Task DeleteOldestFruitAsync(string fruitType);
}