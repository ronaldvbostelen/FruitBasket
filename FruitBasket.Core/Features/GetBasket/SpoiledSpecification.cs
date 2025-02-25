namespace FruitBasket.Core.Features.GetBasket;

public class SpoiledSpecification
{
    public static bool Applies(int fruitCount, int spoiledCount)
    {
        if (fruitCount == 0) throw new DivideByZeroException();
        
        return (double) spoiledCount / fruitCount * 100 > 20;
    }
}