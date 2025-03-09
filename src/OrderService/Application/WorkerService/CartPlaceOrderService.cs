

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
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CartPlaceOrderService(IConnection connection, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connection;
            _channel = _connection.CreateChannelAsync().Result;

            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Console.WriteLine("CartPlaceOrderService is running.");

            await _channel.QueueDeclareAsync(
                queue: RouteKeys.CartPlaceOrder,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.WriteLine("[x] Received {0} at file {1}", message, nameof(CartPlaceOrderService));

                // Deserialize the message
                var cartPlaceOrder = JsonConvert.DeserializeObject<CartPlaceOrder>(message);

                if (cartPlaceOrder != null)
                {
                    // Send the message to the mediator
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await mediator.Send(new CartPlaceOrderCommand(cartPlaceOrder), stoppingToken);
                        Console.WriteLine("[x] Done at file {0}", nameof(CartPlaceOrderService));
                    }

                }
                else
                {
                    Console.WriteLine("[x] Error: Invalid message with object null at file {0}", nameof(CartPlaceOrderService));
                }

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync(RouteKeys.CartPlaceOrder, autoAck: true, consumer: consumer);

            await Task.Delay(-1, stoppingToken);

        }
    }
}