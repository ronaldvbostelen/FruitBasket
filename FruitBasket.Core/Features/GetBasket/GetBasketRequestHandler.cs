using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.GetBasket;

public record GetBasketRequest() : IRequest<GetBasketResponse>;
public record GetBasketResponse(BasketDto Basket);
public class GetBasketRequestHandler(ILogger<GetBasketRequestHandler> logger, IFruitBasketRepository basketRepository, IFruitHealthChecker healthChecker) : IRequestHandler<GetBasketRequest, GetBasketResponse>
{
    public Task<GetBasketResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
    {
        var fruits = basketRepository.List();
        var isSpoiledCount = fruits.Count(fruit => healthChecker.CheckSpoiled(fruit.FruitType, fruit.PurchaseDate));
        var isSpoiled = SpoiledSpecification.Applies(fruits.Count, isSpoiledCount);
        
        logger.LogInformation("Getting basket {basket}", new { fruits.Count, isSpoiled });
        
        return Task.FromResult(new GetBasketResponse(new BasketDto(fruits.Count, isSpoiled)));
    }
}

// - GET /mand geeft { AantallenVanFruit, IsBedorven } : Geeft een overzicht van de fruitmand. De mand is bedorven als meer dan 20% van het fruit bedorven is
