using FluentValidation;
using FruitBasket.Core.Features.AddApple;
using FruitBasket.Core.Features.AddBanana;
using FruitBasket.Core.Features.AddFruit;
using FruitBasket.Core.Features.DeleteFruit;
using FruitBasket.Core.Features.GetFruit;

namespace FruitBasket.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class FruitController(IMediator mediator) : ControllerBase
{
    [HttpGet("{fruittype}")]
    public async Task<IResult> Get([FromRoute] string fruittype)
    {
        var result = await mediator.Send(new GetFruitRequest(fruittype));
        return result.fruit != FruitDto.Empty
            ? Results.Ok(result)
            : Results.NotFound();
    }

    [HttpPut("banana")]
    public async Task<IResult> PutBanana([FromBody] AddBananaDto dto)
    {
        var result = await mediator.Send(dto.ToRequest());
        return Results.Created("/", result);
    }

    [HttpPut("apple")]
    public async Task<IResult> PutApple([FromBody] AddAppleDto dto)
    {
        var result = await mediator.Send(dto.ToRequest());
        return Results.Created("/", result);
    }

    [HttpPut("{fruit}")]
    public async Task<IResult> PutFruit([FromRoute] string fruit, [FromBody] AddFruitDto dto)
    {
        var result = await mediator.Send(dto.ToRequest(fruit));
        return Results.Created("/", result);
    }
    
    [HttpDelete("{fruit}")]
    public async Task<IResult> Delete([FromRoute] string fruit)
    {
        await mediator.Send(new DeleteFruitRequest(fruit));
        return Results.NoContent();
    }
}