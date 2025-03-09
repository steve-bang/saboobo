

using Microsoft.EntityFrameworkCore;
using Saboobo.RabbitMqService.Extensions;
using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.Domain.Shared.Extensions;
using SaBooBo.MigrationService;
using SaBooBo.OrderService.Application.WorkerService;
using SaBooBo.OrderService.Domain.Repositories;
using SaBooBo.OrderService.Infrastructure.Repositories;
using SaBooBo.UserService.Infrastructure;

namespace SaBooBo.MerchantService.Extensions;

public static class DependencyInjection
{
    public static async Task<IHostApplicationBuilder> AddOrderService(this IHostApplicationBuilder builder)
    {
        await builder.AddRabbitMQService();

        builder.Services.AddDbContext<OrderContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.EnrichNpgsqlDbContext<OrderContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<OrderContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        //builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        builder.Services.AddHostedService<CartPlaceOrderService>();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        builder.AddDefaultAuthentication();

        return builder;
    }

}