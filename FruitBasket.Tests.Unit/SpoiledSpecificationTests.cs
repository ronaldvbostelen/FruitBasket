using FruitBasket.Core.Features.GetBasket;
using Shouldly;

namespace FruitBasket.Tests.Unit;

public class SpoiledSpecificationTests
{
    [Theory]
    [InlineData(10, 3, true)] // 30% spoiled (above 20%), should return true
    [InlineData(10, 2, false)] // 20% spoiled (at exact threshold), should return false
    [InlineData(10, 1, false)] // 10% spoiled (below threshold), should return false
    [InlineData(5, 2, true)] // 40% spoiled, should return true
    [InlineData(100, 21, true)] // 21% spoiled (just above 20%), should return true
    [InlineData(100, 20, false)] // 20% spoiled, should return false
    public void Applies_BasedOnSpoiledPercentage_ShouldWorkCorrectly(int fruitCount, int spoiledCount, bool expectedResult)
    {
        // Act
        var result = SpoiledSpecification.Applies(fruitCount, spoiledCount);

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Fact]
    public void Applies_WithZeroFruitCount_ShouldThrowDivideByZeroException()
    {
        // Act & Assert
        var exception = Should.Throw<DivideByZeroException>(() => SpoiledSpecification.Applies(0, 1));
        exception.ShouldNotBeNull();
    }
}
