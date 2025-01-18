
using SaBooBo.Domain.Shared;
using SaBooBo.Product.Domain.AggregatesModel;
using SaBooBo.Product.Domain.Repository;

namespace SaBooBo.Product.Infrastructure.Repository;

public class ToppingRepository(ProductAppContext _dbContext) : IToppingRepository
{
    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<bool> DeleteAsync(Topping topping)
    {
        await Task.CompletedTask;

        if(topping is null) return false;

        _dbContext.Toppings.Remove(topping);

        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var topping = await _dbContext.Toppings.FirstOrDefaultAsync(x => x.Id == id);

        if(topping is null) return false;

        _dbContext.Remove(topping);

        return true;
    }

    public async Task<bool> DeleteByProductIdAsync(Guid productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

        if(product is null || !product.Toppings.Any()) return false;

        _dbContext.RemoveRange(product.Toppings);

        return true;

    }
}