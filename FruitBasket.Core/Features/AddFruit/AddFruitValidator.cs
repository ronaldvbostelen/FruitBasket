using FluentValidation;

namespace FruitBasket.Core.Features.AddFruit;

public class AddFruitValidator : AbstractValidator<AddFruitRequest>
{
    public AddFruitValidator()
    {
        RuleFor(f => f.PurchaseDate)
            .InclusiveBetween(new DateOnly(2020,1,1), DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().Date))
            .NotEmpty();

        RuleFor(f => f.FruitType)
            .MaximumLength(1024);
    }
}