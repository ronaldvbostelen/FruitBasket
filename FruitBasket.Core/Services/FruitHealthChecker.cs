using FruitBasket.Core.Interfaces;
using FruitBasket.Core.Options;
using Microsoft.Extensions.Options;

namespace FruitBasket.Core.Services;

public class FruitHealthChecker(IOptions<FruitSpoilData> options, 
    TimeProvider timeProvider) : IFruitHealthChecker
{
    private readonly BestBeforeDays _bestBeforeDays = new(options.Value.FruitSpoilDays);
    
    public bool CheckSpoiled(string fruitType, DateTime purchaseDate)
    {
        var spoilDays = _bestBeforeDays.GetBestBeforeDaysOrDefault(fruitType);

        return purchaseDate.AddDays(spoilDays) < timeProvider.GetUtcNow();
    }
}

public class BestBeforeDays(IEnumerable<FruitSpoilDays> data)
{
    private readonly int _defaultBestBeforeDays = data.Single(fruitSpoilData => fruitSpoilData.Fruit == "*").BestBeforeDays;

    public int GetBestBeforeDaysOrDefault(string fruitType) =>
        data.SingleOrDefault(fruitSpoilData => fruitSpoilData.Fruit == fruitType)?.BestBeforeDays ??
        _defaultBestBeforeDays;
}