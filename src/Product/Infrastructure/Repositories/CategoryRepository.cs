
using SaBooBo.Domain.Shared;
using SaBooBo.Product.Domain.AggregatesModel;
using SaBooBo.Product.Domain.Repository;

namespace SaBooBo.Product.Infrastructure.Repository;

public class CategoryRepository(ProductAppContext _dbContext) : ICategoryRepository
{
    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<Category> CreateAsync(Category category)
    {
        var result = await _dbContext.Categories.AddAsync(category);

        return result.Entity;
    }

    public Category Update(Category category)
    {
        return _dbContext
            .Categories
            .Update(category)
            .Entity;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _dbContext
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> ListAllAsync(Guid merchantId)
    {
        return await _dbContext
            .Categories
            .Where(c => c.MerchantId == merchantId)
            .ToListAsync();
    }

    public async Task<List<Category>> ListAllAsync()
    {
        return await _dbContext
            .Categories
            .ToListAsync();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var category = await GetByIdAsync(id);

        if (category is null) return false;

        _dbContext.Categories.Remove(category);

        return true;
    }

    public bool Delete(Category category)
    {
        if (category is null) return false;

        _dbContext.Categories.Remove(category);

        return true;
    }
}