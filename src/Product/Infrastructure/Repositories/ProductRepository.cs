
using SaBooBo.Domain.Shared;
using SaBooBo.Product.Domain.Filters;
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

    public async Task<List<Domain.AggregatesModel.Product>> ListAllAsync(ProductFilter filter)
    {
        var query = _dbContext.Products.Where(p => p.MerchantId == filter.MerchantId);

        if (filter.PriceFrom.HasValue)
        {
            query = query.Where(p => p.Price >= filter.PriceFrom);
        }

        if (filter.PriceTo.HasValue)
        {
            query = query.Where(p => p.Price <= filter.PriceTo);
        }


        if (filter.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == filter.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = query.Where(p => p.Name.Contains(filter.Keyword) || (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(filter.Keyword)));
        }

        return await query.ToListAsync();
    }

}