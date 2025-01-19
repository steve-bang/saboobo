
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Domain.Repository;

public interface ICategoryRepository : IRepository
{
    Task<Category> CreateAsync(Category category);

    Task<Category?> GetByIdAsync(Guid id);

    Category Update(Category category);

    Task<List<Category>> ListAllAsync(Guid merchantId);

    Task<List<Category>> ListAllAsync();

    Task<bool> DeleteByIdAsync(Guid id);

    bool Delete(Category category);
}