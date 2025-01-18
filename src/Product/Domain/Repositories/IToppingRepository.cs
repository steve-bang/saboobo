
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Domain.Repository;

public interface IToppingRepository : IRepository
{
    Task<bool> DeleteByIdAsync(Guid id);

    Task<bool> DeleteAsync(Topping topping);

    Task<bool> DeleteByProductIdAsync(Guid productId);

}