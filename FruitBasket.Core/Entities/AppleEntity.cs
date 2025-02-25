using FruitBasket.Core.Features.AddApple;

namespace FruitBasket.Core.Interfaces;

public class AppleEntity : FruitEntity
{
    public AppleEntity()
    {
        FruitType = "Apple";
    }
    
    public AppleType Type { get; set; }
}