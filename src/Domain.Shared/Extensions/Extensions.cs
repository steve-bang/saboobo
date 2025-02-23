
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaBooBo.Domain.Shared.Middlewares;
using SaBooBo.Domain.Shared.Services.Identity;

namespace SaBooBo.Domain.Shared.Extentions;

public static class DomainSharedExtensions
{
    public static IServiceCollection AddServiceDefault(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IGlobalExceptionHandler, GlobalExceptionHandler>();

        services.AddScoped<IIdentityService, IdentityService>();
        return services;
    }

    public static IApplicationBuilder UseServiceDefault(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}