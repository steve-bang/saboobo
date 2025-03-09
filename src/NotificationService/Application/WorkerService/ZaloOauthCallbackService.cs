

using System.Text;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqService.Constants;
using SaBooBo.NotificationService.Application.Features.Commands;
using SaBooBo.NotificationService.Domain.AggregatesModel;
using SaBooBo.OrderService.Clients;

namespace SaBooBo.NotificationService.Application.WorkerService
{
    public class ZaloOAuthCallbackWorkerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ZaloOAuthCallbackWorkerService(IConnection connection, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connection;
            _channel = _connection.CreateChannelAsync().Result;

            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Console.WriteLine("ZaloOAuthCallbackService is running.");

            await _channel.QueueDeclareAsync(
                queue: RouteKeys.ZaloOAuthCallback,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.WriteLine(" [x] Received {0}", message);

                // Deserialize the message
                var zaloOAuthConfig = JsonConvert.DeserializeObject<ZaloOAuthConfigCallback>(message);

                if (zaloOAuthConfig != null)
                {
                    // Send the message to the mediator
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var updateMetadataCommand = new UpdateChannelConfigMetadataCommand(
                               zaloOAuthConfig.MerchantId,
                               ChannelType.Zalo,
                                 new Dictionary<string, string>
                                 {
                                      { ZaloKeysConfig.OAuthCode, zaloOAuthConfig.OauthCode },
                                      { ZaloKeysConfig.AppId, zaloOAuthConfig.OaId }
                                 }
                        );

                        Console.WriteLine($"[x] Updating metadata for merchant {zaloOAuthConfig.MerchantId} and channel Zalo");

                        await mediator.Send(updateMetadataCommand, stoppingToken);
                        Console.WriteLine(" [x] Done");
                    }

                }
                else
                {
                    Console.WriteLine(" [x] Error: Invalid message with object null");
                }

                await Task.Yield();
            };

            await _channel.BasicConsumeAsync(RouteKeys.ZaloOAuthCallback, autoAck: true, consumer: consumer);

            await Task.Delay(-1, stoppingToken);

        }
    }

    public record ZaloOAuthConfigCallback(Guid MerchantId, string OauthCode, string OaId, DateTime SendAt);
}