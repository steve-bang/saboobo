
using SaBooBo.CustomerService.Infrastructure.Repositories;

namespace CustomerService.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddCustomerService(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
    
}