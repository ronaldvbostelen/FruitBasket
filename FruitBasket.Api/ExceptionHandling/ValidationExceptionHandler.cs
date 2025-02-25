using FruitBasket.Core.Behaviors;
using Microsoft.AspNetCore.Diagnostics;

namespace FruitBasket.Api.ExceptionHandling;

internal sealed class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not FluentValidation.ValidationException fluentException)
        {
            return false;
        }

        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path,
            Title = "one or more validation errors occurred.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        problemDetails.Extensions.Add("errors", fluentException.Errors.Select(error => error.ErrorMessage).ToList());

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
