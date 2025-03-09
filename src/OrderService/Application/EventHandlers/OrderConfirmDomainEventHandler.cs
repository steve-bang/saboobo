
using MediatR;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Application.EventHandlers
{
    public class OrderConfirmEventHandler : INotificationHandler<OrderConfirmEvent>
    {
        private readonly ILogger<OrderConfirmEventHandler> _logger;

        public OrderConfirmEventHandler(ILogger<OrderConfirmEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderConfirmEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order confirm: {notification.Order.Id}");
            LoggingUtil.WriteLog($"Order confirm: {notification.Order.Id}");

            // Send notification for OA zalo message.

            return Task.CompletedTask;
        }

    }
}