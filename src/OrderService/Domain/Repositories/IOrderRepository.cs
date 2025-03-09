
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Domain.Repositories;

public interface IOrderRepository : IRepository
{
    Task<Order> CreateAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Order>> GetListAsync(Guid merchantId, CancellationToken cancellationToken = default);

    Task<List<Order>> GetListByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    void Update(Order order, CancellationToken cancellationToken = default);

    void DeleteAsync(Order order, CancellationToken cancellationToken = default);

}