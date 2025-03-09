
using Microsoft.EntityFrameworkCore;
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

    public void DeleteAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Remove(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Orders.FindAsync(id);
    }

    public Task<List<Order>> GetListAsync(Guid merchantId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetListByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await context.Orders.Where(o => o.CustomerId == customerId).ToListAsync(cancellationToken);
    }

    public void Update(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
    }
}