using FluentValidation;

namespace FruitBasket.Core.Features.AddBanana;

public class AddBananaValidator : AbstractValidator<AddBananaRequest>
{
    public AddBananaValidator()
    {
        RuleFor(b => b.PurchaseDate)
            .InclusiveBetween(new DateOnly(2020,1,1), DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().Date))
            .NotEmpty();
        
        RuleFor(b => b.IsOrganic)
            .NotEmpty();
    }
}