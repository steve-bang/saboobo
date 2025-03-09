
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Events;

public class OrderShippingEvent : IDomainEvent 
{
    public Order Order { get; }

    public OrderShippingEvent(Order order)
    {
        Order = order;
    }

    public static OrderShippingEvent Create(Order order)
    {
        return new (order);
    }
}