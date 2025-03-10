using Microsoft.Extensions.DependencyInjection;
using SaBooBo.Clients.Shared.Clients;

namespace SaBooBo.Clients.Shared.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddMerchantClient(this IServiceCollection services)
    {
        services.AddScoped<IMerchantClient, MerchantClient>();
        return services;
    }

    public static IServiceCollection AddUserClient(this IServiceCollection services)
    {
        services.AddScoped<IUserClient, UserClient>();
        return services;
    }

    public static IServiceCollection AddAllClients(this IServiceCollection services)
    {
        services
            .AddUserClient()
            .AddMerchantClient();
            
        return services;
    }
} 