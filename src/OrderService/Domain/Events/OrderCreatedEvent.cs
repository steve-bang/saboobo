
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Events;

public class OrderCreatedEvent : IDomainEvent 
{
    public Order Order { get; }

    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }

    public static OrderCreatedEvent Create(Order order)
    {
        return new (order);
    }
}