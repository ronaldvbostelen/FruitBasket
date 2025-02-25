using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.AddFruit;

public record AddFruitRequest(string FruitType, DateOnly PurchaseDate) : IRequest<AddFruitResponse>;

public record AddFruitResponse;

public class AddFruitRequestHandler(ILogger<AddFruitRequestHandler> logger, IFruitRepository fruitRepository)
    : IRequestHandler<AddFruitRequest, AddFruitResponse>
{
    public async Task<AddFruitResponse> Handle(AddFruitRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding apple {apple}", request);
        _ = await fruitRepository.AddAsync(EntityMapper.MapToEntity(request));
        return new AddFruitResponse();
    }
}