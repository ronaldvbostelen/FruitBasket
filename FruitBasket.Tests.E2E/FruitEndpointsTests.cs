using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using FruitBasket.Tests.E2E;
using Shouldly;

namespace YourApp.Tests;

public class FruitEndpointsTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetFruit_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/fruit/peer");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var result = await response.Content.ReadAsStringAsync();
        result.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task PutApple_WithValidData_Returns_Created()
    {
        // Arrange
        var data = new
        {
            PurchaseDate = "2025-02-24",
            Type = "Elstar"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/fruit/apple", data);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task PutBanana_WithValidData_Returns_Created()
    {
        // Arrange
        var data = new
        {
            PurchaseDate = "2025-02-24",
            IsOrganic = true
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/fruit/banana", data);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task PutFruit_WithValidData_Returns_Created()
    {
        // Arrange
        var data = new
        {
            PurchaseDate = "2025-02-23"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/fruit/pineapple", data);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task PutApple_WithInvalidData_Returns_BadRequest()
    {
        // Arrange
        var data = new
        {
            PurchaseDate = "1999-02-24",
            Type = "Elstar"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/fruit/apple", data);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task DeleteFruit_WithValidData_Returns_NoContent()
    {
        // Arrange & Act
        var response = await _client.DeleteAsync("/api/v1/fruit/pineapple");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteBasket_Returns_NoContent()
    {
        // Arrange & Act
        var response = await _client.DeleteAsync("/api/v1/basket");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetBanana_ShouldReturnBananaData()
    {
        // Arrange & Act
        var response = await _client.GetAsync("/api/v1/fruit/banana");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}