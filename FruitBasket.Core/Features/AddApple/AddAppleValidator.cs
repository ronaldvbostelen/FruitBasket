using FluentValidation;

namespace FruitBasket.Core.Features.AddApple;

public class AddAppleValidator : AbstractValidator<AddAppleRequest>
{
    public AddAppleValidator()
    {
        RuleFor(r => r.PurchaseDate)
            .InclusiveBetween(new DateOnly(2020,1,1), DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().Date))
            .NotEmpty();
        
        RuleFor(r => r.Type)
            .IsInEnum();
    }
}