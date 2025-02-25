using Microsoft.AspNetCore.Diagnostics;

namespace FruitBasket.Api.ExceptionHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path,
            Title = exception.Message,
            Detail = "Try again later or contact support.",
            Status = httpContext.Response.StatusCode
        };

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}