
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SaBooBo.Domain.Shared.Extentions;

namespace SaBooBo.Domain.Shared.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

     private readonly IServiceProvider _serviceProvider;

    public ExceptionHandlingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = _serviceProvider.CreateScope();
        var exceptionHandler = scope.ServiceProvider.GetRequiredService<IGlobalExceptionHandler>();

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[x] Exception: ");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine(ex.InnerException?.Message);
            await exceptionHandler.TryHandleAsync(context, ex);
        }
    }
}