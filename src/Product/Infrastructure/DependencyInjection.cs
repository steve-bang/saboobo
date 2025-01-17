
using AppHost.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaBooBo.MigrationService;

namespace SaBooBo.Product.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {

        builder.Services.AddDbContext<ProductAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(DbConnections.ProductName))
        );
        builder.EnrichNpgsqlDbContext<ProductAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<ProductAppContext>();

        return builder;
    }
}