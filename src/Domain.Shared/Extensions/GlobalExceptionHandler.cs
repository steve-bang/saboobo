
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.Domain.Shared.Extentions;

public interface IGlobalExceptionHandler
{
    Task TryHandleAsync(HttpContext httpContext, Exception exception);
}

public class GlobalExceptionHandler : IGlobalExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task TryHandleAsync(
        HttpContext httpContext,
        Exception exception)
    {
        // Log the exception
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var exceptionHandler = exception as SaBooBoException;

        if (exceptionHandler is null)
        {
            exceptionHandler = new SaBooBoException(exception);
        }

        // Set the response status code to the exception's status code
        httpContext.Response.StatusCode = exceptionHandler.Error.HttpStatus;
        httpContext.Response.ContentType = "application/json";

        // Await the WriteAsJsonAsync method to ensure the response is written before the method returns
        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(exceptionHandler.Error)
        );

    }
}
