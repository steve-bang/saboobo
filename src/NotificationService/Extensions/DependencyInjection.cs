using Microsoft.EntityFrameworkCore;
using Saboobo.RabbitMqService.Extensions;
using SaBooBo.Clients.Shared.Extensions;
using SaBooBo.Domain.Shared.Behaviour;
using SaBooBo.Domain.Shared.Extensions;
using SaBooBo.Domain.Shared.Configurations;
using SaBooBo.MigrationService;
using SaBooBo.NotificationService.Domain.Repositories;
using SaBooBo.NotificationService.Domain.Services;
using SaBooBo.NotificationService.Infrastructure;
using SaBooBo.NotificationService.Infrastructure.Repositories;
using SaBooBo.NotificationService.Infrastructure.Services;
using SaBooBo.OrderService.Clients;

namespace SaBooBo.NotificationService.Extensions;

public static class DependencyInjection
{
    public static async Task<IHostApplicationBuilder> AddNotificationServices(this IHostApplicationBuilder builder)
    {
        await builder.AddRabbitMQService();

        // Add ServiceConfiguration
        var serviceConfig = new ServiceConfiguration();
        //builder.Configuration.GetSection("ServiceConfiguration").Bind(serviceConfig);
        builder.Services.AddSingleton(serviceConfig);

        builder.Services.AddDbContext<NotificationAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        //builder.EnrichNpgsqlDbContext<NotificationAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<NotificationAppContext>();


        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        //builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        builder.Services.AddScoped<IChannelRepository, ChannelRepository>();

        builder.Services.AddScoped<IChannelConfigRepository, ChannelConfigRepository>();

        builder.Services.AddScoped<ISmsService, SmsService>();

        // Add the client using gRPC protocol
        builder.Services.AddAllClients();

        // Add the clients
        builder.AddClientServices();

        builder.AddDefaultAuthentication();

        return builder;
    }

    public static IHostApplicationBuilder AddClientServices(this IHostApplicationBuilder builder)
    {
        // Register the clients
        builder.Services.AddHttpClient(ZaloClientNames.ZaloAuthClient, (serviceProvider, client) =>
        {

            var settings = serviceProvider.GetRequiredService<IConfiguration>();

            string? baseUrl = settings["Clients:ZaloAuth"];

            if (baseUrl is null)
            {
                throw new ArgumentNullException("Please provide the base url for the client in the appsettings.json file at Clients:ZaloAuthClient");
            }

            client.BaseAddress = new Uri(baseUrl);
        });

        builder.Services.AddHttpClient(ZaloClientNames.ZaloOpenApiClient, (serviceProvider, client) =>
        {

            var settings = serviceProvider.GetRequiredService<IConfiguration>();

            string? baseUrl = settings["Clients:ZaloOpenApi"];

            if (baseUrl is null)
            {
                throw new ArgumentNullException("Please provide the base url for the client in the appsettings.json file at Clients:ZaloOpenApi");
            }

            client.BaseAddress = new Uri(baseUrl);
        });

        builder.Services.AddScoped<IZaloClient, ZaloClient>();


        return builder;
    }

}