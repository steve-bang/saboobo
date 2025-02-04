
using SaBooBo.MerchantService.Domain.Repositories;

namespace SaBooBo.MerchantService.Infrastructure.Repositories;

public class MerchantRepository(
        MerchantAppContext _dbContext
) : IMerchantRepository
{
    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<Merchant> CreateAsync(Merchant merchant)
    {
        var result = await _dbContext.Merchants.AddAsync(merchant);

        return result.Entity;
    }

    public bool Delete(Merchant merchant)
    {
        if (merchant == null)
        {
            return false;
        }

        var result = _dbContext.Merchants.Remove(merchant);

        return result.State == EntityState.Deleted;
    }

    public Task<List<Merchant>> GetAllAsync()
    {
        return _dbContext.Merchants.ToListAsync();
    }

    public async Task<Merchant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Merchants.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Merchant?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Merchants.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public Task<Merchant> UpdateAsync(Merchant merchant)
    {
        var result = _dbContext.Merchants.Update(merchant);

        return Task.FromResult(result.Entity);
    }
}
