using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.AddBanana;

public record AddBananaRequest(DateOnly PurchaseDate, bool IsOrganic) : IRequest<AddBananaResponse>;
public record AddBananaResponse;

public class AddBananaRequestHandler(ILogger<AddBananaRequestHandler> logger, IFruitRepository fruitRepository) : IRequestHandler<AddBananaRequest, AddBananaResponse>
{
    public async Task<AddBananaResponse> Handle(AddBananaRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding apple {apple}", request);
        _ = await fruitRepository.AddAsync(EntityMapper.MapToEntity(request));
        return new AddBananaResponse();
    }
}