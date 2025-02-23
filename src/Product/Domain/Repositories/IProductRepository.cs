
using SaBooBo.Product.Domain.Filters;

namespace SaBooBo.Product.Domain.Repository;

public interface IProductRepository : IRepository
{
    Task<AggregatesModel.Product> CreateAsync(AggregatesModel.Product product);

    Task<AggregatesModel.Product?> GetByIdAsync(Guid id);

    AggregatesModel.Product UpdateByIdAsync(AggregatesModel.Product product);

    Task<bool> DeleteByIdAsync(Guid id);

    bool Delete(AggregatesModel.Product product);

    Task<List<AggregatesModel.Product>> ListAllAsync(Guid merchantId);

    Task<List<AggregatesModel.Product>> ListAllAsync();

    Task<List<AggregatesModel.Product>> ListAllAsync(ProductFilter filter);
}