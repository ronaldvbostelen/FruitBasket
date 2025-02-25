using System.Linq.Expressions;
using Azure.Data.Tables;
using FruitBasket.Core.Interfaces;
using FruitBasket.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FruitBasket.Infrastructure.Services;

public class FruitTableStorageService : IFruitRepository
{
    private readonly ILogger<FruitTableStorageService> _logger;
    private readonly TableClient _tableClient;

    public FruitTableStorageService(ILogger<FruitTableStorageService> logger, IOptions<TableStorageOptions> options)
    {
        _logger = logger;
        _tableClient = new TableClient(options.Value.ConnectionString, options.Value.TableName);
        _tableClient.CreateIfNotExists();
    }

    public TEntity? FindOldestFruit<TEntity>(Func<TEntity, bool> predicate) where TEntity : FruitEntity =>
        _tableClient
            .Query<TEntity>()
            .OrderBy(e => e.PurchaseDate)
            .FirstOrDefault(predicate);

    public async Task<FruitEntity> AddAsync(FruitEntity fruit)
    {
        try
        {        
            await _tableClient.AddEntityAsync(fruit);
            _logger.LogInformation("Added fruit {fruit}", fruit);
            return fruit;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding fruit {fruit}", fruit);
            throw;
        }
    }

    public FruitEntity? FindAsync(Func<FruitEntity, bool> predicate) =>
        _tableClient
            .Query<FruitEntity>()
            .OrderBy(entity => entity.PurchaseDate)
            .FirstOrDefault(predicate);
}