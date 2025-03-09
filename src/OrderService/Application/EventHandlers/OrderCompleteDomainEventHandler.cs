
using MediatR;
using SaBooBo.Domain.Shared.Utils;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Application.EventHandlers
{
    public class OrderCompleteEventHandler : INotificationHandler<OrderCompleteEvent>
    {
        private readonly ILogger<OrderCompleteEventHandler> _logger;

        public OrderCompleteEventHandler(ILogger<OrderCompleteEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCompleteEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order completed: {notification.Order.Id}");
            LoggingUtil.WriteLog($"Order completed: {notification.Order.Id}");

            // Send notification for OA zalo message.

            return Task.CompletedTask;
        }

    }
}