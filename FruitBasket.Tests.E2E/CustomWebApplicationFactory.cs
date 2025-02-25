using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace FruitBasket.Tests.E2E;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace services as needed for testing

            // Example: Replace DbContext with an in-memory database for tests
            // services.AddDbContext<YourDbContext>(options =>
            // {
            //     options.UseInMemoryDatabase("TestDb");
            // });

            // Optionally remove any services here if needed
            // services.RemoveAll(typeof(SomeService));
        });
    }
}
