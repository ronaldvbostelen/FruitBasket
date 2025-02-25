using FluentValidation;

namespace FruitBasket.Core.Features.GetFruit;

public class GetFruitValidator : AbstractValidator<GetFruitRequest>
{
    public GetFruitValidator()
    {
        RuleFor(f => f.FruitType)
            .MaximumLength(1024)
            .NotEmpty();
    }
}