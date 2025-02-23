
using MediatR;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order created: {OrderId}", notification.Order.Id);

            // Send notification for OA zalo message.

            return Task.CompletedTask;
        }

    }
}