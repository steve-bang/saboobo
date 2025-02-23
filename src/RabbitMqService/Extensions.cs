
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMqService.Producers;

namespace Saboobo.RabbitMqService.Extensions;

public static class RabbitMQExtension
{
    public static async Task<IHostApplicationBuilder> AddRabbitMQService(this IHostApplicationBuilder builder)
    {
        string? hostName = builder.Configuration["RabbitMQ:HostName"];

        string? port = builder.Configuration["RabbitMQ:Port"];

        if (string.IsNullOrEmpty(hostName))
        {
            throw new ArgumentNullException("RabbitMQ:HostName is not set in the configuration file");
        }

        if (string.IsNullOrEmpty(port))
        {
            throw new ArgumentNullException("RabbitMQ:Port is not set in the configuration file");
        }

        var factory = new ConnectionFactory
        {
            HostName = hostName,
            Port = int.Parse(port),
            // UserName = builder.Configuration["RabbitMQ:UserName"],
            // Password = builder.Configuration["RabbitMQ:Password"]
        };
        var connection = await factory.CreateConnectionAsync();

        builder.Services.AddSingleton(connection);

        builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

        return builder;
    }
}