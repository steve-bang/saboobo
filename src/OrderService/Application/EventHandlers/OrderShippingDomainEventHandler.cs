
using MediatR;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Application.EventHandlers
{
    public class OrderShippingEventHandler : INotificationHandler<OrderShippingEvent>
    {
        private readonly ILogger<OrderShippingEventHandler> _logger;

        public OrderShippingEventHandler(ILogger<OrderShippingEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderShippingEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order shipping: {notification.Order.Id}");
            LoggingUtil.WriteLog($"Order shipping: {notification.Order.Id}");

            // Send notification for OA zalo message.

            return Task.CompletedTask;
        }

    }
}