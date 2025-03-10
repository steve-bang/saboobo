
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using RabbitMqService.Constants;
using RabbitMqService.Producers;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Application.EventHandlers
{
    public class OrderCreatedEventHandler(
        IRabbitMqProducer _rabbitMqProducer
    ) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {

            LoggingUtil.WriteLog($"Order created: {notification.Order.Id}");

            // Send message to RabbitMQ
            // Use retry logic to send message to RabbitMQ

            const int maxRetries = 3;
            const int delayMilliseconds = 1000;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    // Serialize the order object to a JSON string and options Enums
                    var objectSerialize = JsonSerializer.Serialize(notification.Order, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new JsonStringEnumConverter() }
                    });

                    // Send message to RabbitMQ
                    await _rabbitMqProducer.PublishAsync(
                        exchange: string.Empty,
                        routingKey: RouteKeys.OrderChangeStatus,
                        objectSerialize
                    );

                    LoggingUtil.WriteLog($"Sent order created event to RabbitMQ for order {notification.Order.Id}");
                    return; // Success - exit the retry loop
                }
                catch (Exception ex)
                {
                    LoggingUtil.WriteLog(
                        message: $"Failed to publish order created event. Attempt {i + 1} of {maxRetries}",
                        fileName: nameof(OrderCreatedEventHandler),
                        exception: ex
                    );

                    if (i == maxRetries - 1) // Last attempt
                    {
                        throw; // Re-throw if all retries failed
                    }

                    await Task.Delay(delayMilliseconds * (i + 1), cancellationToken); // Exponential backoff
                }
            }
        }
    }
}