
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Repositories;

public interface IOrderRepository : IRepository
{
    Task<Order> CreateAsync(Order order);
}