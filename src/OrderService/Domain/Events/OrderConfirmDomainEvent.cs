
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Events;

public class OrderConfirmEvent : IDomainEvent 
{
    public Order Order { get; }

    public OrderConfirmEvent(Order order)
    {
        Order = order;
    }

    public static OrderConfirmEvent Create(Order order)
    {
        return new (order);
    }
}