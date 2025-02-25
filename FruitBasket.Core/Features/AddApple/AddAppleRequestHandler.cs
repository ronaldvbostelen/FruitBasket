using FruitBasket.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FruitBasket.Core.Features.AddApple;

public record AddAppleRequest(DateOnly PurchaseDate, AppleType Type) : IRequest<AddAppleResponse>;
public record AddAppleResponse;

public class AddAppleRequestHandler(ILogger<AddAppleRequestHandler> logger,
    IFruitRepository fruitRepository) 
    : IRequestHandler<AddAppleRequest, AddAppleResponse>
{

    public async Task<AddAppleResponse> Handle(AddAppleRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding apple {apple}", request);
        _ = await fruitRepository.AddAsync(EntityMapper.MapToEntity(request));
        return new AddAppleResponse();
    }
}