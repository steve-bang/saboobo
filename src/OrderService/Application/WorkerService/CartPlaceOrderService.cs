

using System.Text;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService.Constants;
using SaBooBo.OrderService.Application.Features.Commands;

namespace SaBooBo.OrderService.Application.WorkerService
{
    public class CartPlaceOrderService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        private readonly IMediator _mediator;

        public CartPlaceOrderService(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateChannelAsync().Result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Console.WriteLine("CartPlaceOrderService is running.");

            await _channel.QueueDeclareAsync(
                queue: RouteKey.CartPlaceOrder,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.WriteLine(" [x] Received {0}", message);
                Console.WriteLine(" [x] Done");

                // Deserialize the message
                var cartPlaceOrder = JsonConvert.DeserializeObject<CartPlaceOrder>(message);

                if (cartPlaceOrder != null)
                {
                    // Send the message to the mediator
                    await _mediator.Send(new CartPlaceOrderCommand(cartPlaceOrder));
                }
                else 
                {
                    Console.WriteLine(" [x] Error: Invalid message with object null");
                } 

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync(RouteKey.CartPlaceOrder, autoAck: true, consumer: consumer);

            await Task.Delay(-1, stoppingToken);

        }
    }
}