using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.DeleteFruit;

public record DeleteFruitRequest(string FruitType) : IRequest<DeleteFruitResponse>;
public record DeleteFruitResponse;

public class DeleteFruitRequestHandler(ILogger<DeleteFruitRequestHandler> logger,
    IFruitBasketRepository fruitBasketRepository) : IRequestHandler<DeleteFruitRequest, DeleteFruitResponse>
{
    public async Task<DeleteFruitResponse> Handle(DeleteFruitRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting fruit {fruit}", request);
        await fruitBasketRepository.DeleteOldestFruitAsync(request.FruitType);
        return new DeleteFruitResponse();
    }
}