namespace FruitBasket.Core.Interfaces;

public interface IFruitRepository
{
    TEntity? FindOldestFruit<TEntity>(Func<TEntity, bool> predicate) where TEntity : FruitEntity;
    Task<FruitEntity> AddAsync(FruitEntity fruit);
}