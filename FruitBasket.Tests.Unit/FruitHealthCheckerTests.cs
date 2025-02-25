using FruitBasket.Core.Options;
using FruitBasket.Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;

namespace FruitBasket.Tests.Unit;

public class FruitHealthCheckerTests
{
    private readonly Mock<TimeProvider> _timeProviderMock;
    private readonly IOptions<FruitSpoilData> _optionsMock;
    private readonly FruitHealthChecker _fruitHealthChecker;

    public FruitHealthCheckerTests()
    {
        // Mock TimeProvider
        _timeProviderMock = new Mock<TimeProvider>();

        // Fake Best Before Configurations
        var fruitSpoilData = new FruitSpoilData
        {
            FruitSpoilDays =
            [
                new("Apple", 10),
                new("Banana", 5),
                new("*", 7)
            ]
        };

        // Mock IOptions<FruitSpoilData>
        _optionsMock = Options.Create(fruitSpoilData);

        // Initialize FruitHealthChecker with mocks
        _fruitHealthChecker = new FruitHealthChecker(_optionsMock, _timeProviderMock.Object);
    }

    [Fact]
    public void CheckSpoiled_ShouldReturnTrue_WhenFruitIsSpoiled()
    {
        // Arrange
        var purchaseDate = new DateTime(2023, 10, 1);
        _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(new DateTime(2023, 10, 12));

        // Act
        var result = _fruitHealthChecker.CheckSpoiled("Apple", purchaseDate);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CheckSpoiled_ShouldReturnFalse_WhenFruitIsNotSpoiled()
    {
        // Arrange
        var purchaseDate = new DateTime(2023, 10, 1);
        _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(new DateTime(2023, 10, 8));

        // Act
        var result = _fruitHealthChecker.CheckSpoiled("Apple", purchaseDate);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CheckSpoiled_ShouldUseDefaultBestBeforeDays_WhenFruitTypeIsNotListed()
    {
        // Arrange
        var purchaseDate = new DateTime(2023, 10, 1);
        _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(new DateTime(2023, 10, 9)); // After default (7 days)

        // Act
        var result = _fruitHealthChecker.CheckSpoiled("UnknownFruit", purchaseDate);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CheckSpoiled_ShouldReturnFalse_WhenWithinDefaultBestBeforeDays_ForUnknownFruit()
    {
        // Arrange
        var purchaseDate = new DateTime(2023, 10, 1);
        _timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(new DateTime(2023, 10, 6)); // Before default (7 days)

        // Act
        var result = _fruitHealthChecker.CheckSpoiled("UnknownFruit", purchaseDate);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CheckSpoiled_ShouldHandleBoundaryCase_ForSpoilDate()
    {
        // Arrange
        var purchaseDate = new DateTime(2023, 10, 1);
        _timeProviderMock.Setup(tp => tp.GetUtcNow())
            .Returns(new DateTime(2023, 10, 11)); // Exactly on the spoil date for Apple (10 days)

        // Act
        var result = _fruitHealthChecker.CheckSpoiled("Apple", purchaseDate);

        // Assert
        result.ShouldBeFalse();
    }
}