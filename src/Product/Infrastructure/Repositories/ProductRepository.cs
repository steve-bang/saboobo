
using SaBooBo.Domain.Shared;
using SaBooBo.Product.Domain.Repository;

namespace SaBooBo.Product.Infrastructure.Repository;

public class ProductRepository(ProductAppContext _dbContext) : IProductRepository
{
    public IUnitOfWork UnitOfWork => _dbContext;


    public async Task<Domain.AggregatesModel.Product> CreateAsync(Domain.AggregatesModel.Product product)
    {
        var created = await _dbContext.Products.AddAsync(product);

        return created.Entity;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var product = await GetByIdAsync(id);

        if (product == null) return false;

        return Delete(product);
    }

    public bool Delete(Domain.AggregatesModel.Product product)
    {
        var result = _dbContext.Products.Remove(product);

        return result.State == EntityState.Deleted;
    }

    public async Task<Domain.AggregatesModel.Product?> GetByIdAsync(Guid id)
    {
        return await _dbContext
                    .Products
                    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Domain.AggregatesModel.Product>> ListAllAsync(Guid merchantId)
    {
        return await _dbContext
                    .Products
                    .ToListAsync();
    }

    public async Task<List<Domain.AggregatesModel.Product>> ListAllAsync()
    {
        return await _dbContext
                    .Products
                    .ToListAsync();
    }

    public Domain.AggregatesModel.Product UpdateByIdAsync(Domain.AggregatesModel.Product product)
    {
        return _dbContext
                .Products
                .Update(product)
                .Entity;
    }
}