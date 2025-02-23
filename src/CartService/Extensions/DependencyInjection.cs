
using Microsoft.EntityFrameworkCore;
using Saboobo.RabbitMqService.Extensions;
using SaBooBo.CartService.Infrastructure;
using SaBooBo.CartService.Infrastructure.Repository;
using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.Domain.Shared.Extensions;
using SaBooBo.MigrationService;

namespace SaBooBo.MerchantService.Extensions;

public static class DependencyInjection
{
    public static async Task<IHostApplicationBuilder> AddCartService(this IHostApplicationBuilder builder)
    {
        
        await builder.AddRabbitMQService();

        builder.Services.AddDbContext<CartAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<CartAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<CartAppContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        //builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        builder.Services.AddScoped<ICartRepository, CartRepository>();

        builder.AddDefaultAuthentication();

        return builder;
    }

}