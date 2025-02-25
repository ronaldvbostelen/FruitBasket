using Azure.Data.Tables;
using FruitBasket.Core.Interfaces;
using FruitBasket.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FruitBasket.Infrastructure.Services;

public class BasketTableStorageService : IFruitBasketRepository
{
    private readonly ILogger<BasketTableStorageService> _logger;
    private readonly TableClient _tableClient;

    public BasketTableStorageService(ILogger<BasketTableStorageService> logger, IOptions<TableStorageOptions> options)
    {
        _logger = logger;
        _tableClient = new TableClient(options.Value.ConnectionString, options.Value.TableName);
        _tableClient.CreateIfNotExists();
    }

    public List<FruitEntity> List()
    {
        try
        {
            return _tableClient
                .Query<FruitEntity>()
                .ToList();
        }
        catch (Exception e)
        { 
            _logger.LogError(e, "Error listing basket");
            throw;
        }
    }

    public async Task DeleteAllFruitAsync()
    {
        try
        {
            _logger.LogInformation("Deleting all fruits");
            foreach (var fruit in _tableClient.Query<FruitEntity>())
            {
                await _tableClient.DeleteEntityAsync(fruit);
            }
        }
        catch (Exception e)
        { 
            _logger.LogError(e, "Error deleting basket");
            throw;
        }
    }

    public async Task DeleteOldestFruitAsync(string fruitType)
    {
        try
        {
            var oldestPurchase = _tableClient.Query<FruitEntity>()
                .Where(entity => entity.FruitType.Equals(fruitType, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(entity => entity.PurchaseDate)
                .FirstOrDefault();

            if (oldestPurchase == null)
                return;
            
            _logger.LogInformation("Deleting oldest fruits for {fruitType}: {RowKey}", fruitType, oldestPurchase.RowKey);

            await _tableClient.DeleteEntityAsync(oldestPurchase);
        }
        catch (Exception e)
        { 
            _logger.LogError(e, "Error deleting oldest fruits for {fruitType}", fruitType);
            throw;
        }
    }
}