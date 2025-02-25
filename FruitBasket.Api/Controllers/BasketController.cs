using FruitBasket.Core.Features.DeleteBasket;
using FruitBasket.Core.Features.GetBasket;

namespace FruitBasket.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class BasketController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> Get()
    {
        var result = await mediator.Send(new GetBasketRequest());
        return Results.Ok(result);
    }

    [HttpDelete]
    public async Task<IResult> Delete()
    {
        await mediator.Send(new DeleteBasketRequest());
        return Results.NoContent();
    }
}