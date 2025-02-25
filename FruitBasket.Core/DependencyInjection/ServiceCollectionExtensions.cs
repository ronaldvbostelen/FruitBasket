using FluentValidation;
using FruitBasket.Core.Behaviors;
using FruitBasket.Core.Features.AddApple;
using FruitBasket.Core.Interfaces;
using FruitBasket.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FruitBasket.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(opts =>
        {
            opts.RegisterServicesFromAssemblyContaining<AddAppleRequestHandler>();
            opts.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
            opts.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IFruitHealthChecker, FruitHealthChecker>();
        services.AddValidatorsFromAssembly(typeof(AddAppleValidator).Assembly);        
        return services;
    }
}