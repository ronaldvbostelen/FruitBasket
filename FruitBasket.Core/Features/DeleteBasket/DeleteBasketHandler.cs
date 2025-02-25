using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.DeleteBasket;

public record DeleteBasketRequest : IRequest<DeleteBasketResponse>;
public record DeleteBasketResponse;

public class DeleteBasketHandler(ILogger<DeleteBasketHandler> logger, IFruitBasketRepository fruitBasketRepository) : IRequestHandler<DeleteBasketRequest, DeleteBasketResponse>
{
    public async Task<DeleteBasketResponse> Handle(DeleteBasketRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting fruit from basket");
        await fruitBasketRepository.DeleteAllFruitAsync();
        return new DeleteBasketResponse();
    }
}