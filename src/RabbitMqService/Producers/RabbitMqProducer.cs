
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMqService.Producers;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly IChannel _channel;
    private readonly IConnection _connection;

    public RabbitMqProducer(IConnection connection)
    {
        _connection = connection;
        _channel = _connection.CreateChannelAsync().Result;
    }

    public async Task PublishAsync(string exchange, string routingKey, string message)
    {
        try
        {
            await _channel.QueueDeclareAsync(queue: routingKey, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                body: body);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while publishing the message");
            Console.WriteLine(ex.Message);
        }
    }

    public async Task PublishAsync(string exchange, string routingKey, object message)
    {
        try
        {
            var json = JsonConvert.SerializeObject(message);

            Console.WriteLine($"Publishing message to RabbitMQ: {json}");

            byte[] body = Encoding.UTF8.GetBytes(json);

            await _channel.QueueDeclareAsync(queue: routingKey, durable: false, exclusive: false, autoDelete: false, arguments: null);

            await _channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                body: body);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while publishing the message");
            Console.WriteLine(ex.Message);
        }
    }
}