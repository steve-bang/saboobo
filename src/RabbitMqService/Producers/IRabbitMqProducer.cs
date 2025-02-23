
namespace RabbitMqService.Producers;

public interface IRabbitMqProducer
{
    /// <summary>
    /// Publish a message to the RabbitMQ server.
    /// </summary>
    Task PublishAsync(string exchange, string routingKey, string message);

    /// <summary>
    /// Publish a message to the RabbitMQ server.
    /// </summary>
    Task PublishAsync(string exchange, string routingKey, object message);
    
}