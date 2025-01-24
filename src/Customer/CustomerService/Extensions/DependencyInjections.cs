
using Microsoft.EntityFrameworkCore;
using SaBooBo.CustomerService.Infrastructure;
using SaBooBo.CustomerService.Infrastructure.Repositories;
using SaBooBo.MigrationService;

namespace CustomerService.Extensions;

public static class DependencyInjections
{
    public static IHostApplicationBuilder AddCustomerService(this IHostApplicationBuilder builder)
    {

        builder.Services.AddDbContext<CustomerAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<CustomerAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<CustomerAppContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            //config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

        return builder;
    }

}