using FruitBasket.Core.Extensions;
using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.GetFruit;

public record GetFruitRequest(string FruitType) : IRequest<GetFruitResponse>;
public record GetFruitResponse(FruitDto fruit);

public class GetFruitRequestHandler(ILogger<GetFruitRequestHandler> logger, 
    IFruitHealthChecker healthChecker, 
    IFruitRepository fruitRepository) : IRequestHandler<GetFruitRequest, GetFruitResponse>
{
    public Task<GetFruitResponse> Handle(GetFruitRequest request, CancellationToken cancellationToken)
    {
        var fruit = request.FruitType.CapitalizeFirstLetter() switch
        {
            "Banana" => FindFruit<BananaEntity>(request.FruitType, EntityMapper.MapFromBanana),
            "Apple" => FindFruit<AppleEntity>(request.FruitType, EntityMapper.MapFromApple),
            _ => FindFruit<FruitEntity>(request.FruitType, EntityMapper.MapFromFruit)
        };
        
        if (fruit == FruitDto.Empty)
        {
            logger.LogInformation("Fruit {fruit} not found", request.FruitType);
            return Task.FromResult(new GetFruitResponse(fruit));
        }
        
        fruit.IsSpoiled = healthChecker.CheckSpoiled(request.FruitType, fruit.PurchaseDate);
        
        logger.LogInformation("Fruit {fruit} is spoiled: {spoiled}", request.FruitType, fruit.IsSpoiled);
        
        return Task.FromResult(new GetFruitResponse(fruit));
    }

    private FruitDto FindFruit<T>(string fruitType, Func<T, FruitDto> map) where T : FruitEntity
    {
        var fruit = fruitRepository.FindOldestFruit<T>(e =>
            e.FruitType.Equals(fruitType, StringComparison.CurrentCultureIgnoreCase));

        if (fruit == null)
        {
            return FruitDto.Empty;
        }

        return map(fruit);
    }
}