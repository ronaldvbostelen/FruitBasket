using FruitBasket.Core.DependencyInjection;
using FruitBasket.Core.Interfaces;
using FruitBasket.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FruitBasket.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFruitRepository, FruitTableStorageService>();
        services.AddScoped<IFruitBasketRepository, BasketTableStorageService>();

        return services.AddCore();
    }
}