namespace FruitBasket.Core.Interfaces;

public interface IFruitHealthChecker
{
    bool CheckSpoiled(string fruitType, DateTime purchaseDate);
}