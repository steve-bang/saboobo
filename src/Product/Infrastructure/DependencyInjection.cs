
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaBooBo.MigrationService;
using SaBooBo.Product.Domain.Repository;
using SaBooBo.Product.Infrastructure.Repository;

namespace SaBooBo.Product.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {

        builder.Services.AddDbContext<ProductAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.EnrichNpgsqlDbContext<ProductAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<ProductAppContext>();

        #region Repositories
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IToppingRepository, ToppingRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        #endregion

        return builder;
    }
}