
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Events;

public class OrderCompleteEvent : IDomainEvent 
{
    public Order Order { get; }

    public OrderCompleteEvent(Order order)
    {
        Order = order;
    }

    public static OrderCompleteEvent Create(Order order)
    {
        return new (order);
    }
}