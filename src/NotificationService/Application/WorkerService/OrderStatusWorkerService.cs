
using System.Text;
using System.Text.Json;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService.Constants;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.NotificationService.Application.Features.Commands;
using SaBooBo.NotificationService.Domain.Models;
using SaBooBo.NotificationService.Models;

namespace SaBooBo.NotificationService.Application.WorkerService;

/// <summary>
/// This worker service is used to consume the order change status message from RabbitMQ.
/// </summary>
public class OrderStatusWorkerService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OrderStatusWorkerService(IConnection connection, IServiceScopeFactory serviceScopeFactory)
    {
        _connection = connection;
        _channel = _connection.CreateChannelAsync().Result;

        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("OrderStatusWorkerService is running.");

        await _channel.QueueDeclareAsync(
            queue: RouteKeys.OrderChangeStatus,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());

            LoggingUtil.WriteLog($"Received message: {message}", nameof(OrderStatusWorkerService));

            // Deserialize the message
            var order = JsonSerializer.Deserialize<Order>(message);

            LoggingUtil.WriteLog($"Order data deserialized: {order}", nameof(OrderStatusWorkerService));

            // Send notification for OA zalo message.
            if (order != null)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new SendMessageOrderCommand(order));
            }
            else 
            {
                LoggingUtil.WriteLog("Order data is null.", nameof(OrderStatusWorkerService));
            }

            await Task.Yield();
        };

        // Consume the message from RabbitMQ
        await _channel.BasicConsumeAsync(RouteKeys.OrderChangeStatus, autoAck: true, consumer: consumer);

        // Keep the service running indefinitely
        await Task.Delay(-1, stoppingToken);

    }
}