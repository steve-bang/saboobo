
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaBooBo.Domain.Shared.Middlewares;

namespace SaBooBo.Domain.Shared.Extentions;

public static class DomainSharedExtensions
{
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddScoped<IGlobalExceptionHandler, GlobalExceptionHandler>();
        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}