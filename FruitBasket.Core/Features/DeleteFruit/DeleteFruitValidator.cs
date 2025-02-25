using FluentValidation;

namespace FruitBasket.Core.Features.DeleteFruit;

public class DeleteFruitValidator : AbstractValidator<DeleteFruitRequest>
{
    public DeleteFruitValidator()
    {
        RuleFor(d => d.FruitType)
            .MaximumLength(1024)
            .NotNull();
    }
}