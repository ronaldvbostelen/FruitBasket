using System.Text.Json.Serialization;
using FruitBasket.Api.ExceptionHandling;
using FruitBasket.Core.Options;
using FruitBasket.Infrastructure.DependencyInjection;
using FruitBasket.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOptions<TableStorageOptions>()
    .Bind(builder.Configuration.GetSection(nameof(TableStorageOptions)));

builder.Services.AddOptions<FruitSpoilData>()
    .Bind(builder.Configuration.GetSection(nameof(FruitSpoilData)));


builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }