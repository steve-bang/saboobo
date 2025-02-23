
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;
using SaBooBo.OrderService.Domain.Repositories;
using SaBooBo.UserService.Infrastructure;

namespace SaBooBo.OrderService.Infrastructure.Repositories;

public class OrderRepository(
    OrderContext context
) : IOrderRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<Order> CreateAsync(Order order)
    {
        var result = await context.Orders.AddAsync(order);

        return result.Entity;
    }
}