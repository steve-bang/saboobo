
using SaBooBo.MerchantService.Domain.Repositories;

namespace SaBooBo.MerchantService.Infrastructure.Repositories;

public class MerchantProviderSettingRepository : IMerchantProviderSettingRepository
{
    private readonly MerchantAppContext _dbContext;

    public MerchantProviderSettingRepository(MerchantAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<MerchantProviderSetting> CreateAsync(MerchantProviderSetting merchantProviderSetting)
    {
        var result = await _dbContext.MerchantProviderSettings.AddAsync(merchantProviderSetting);

        return result.Entity;
    }

    public bool Delete(MerchantProviderSetting merchantProviderSetting)
    {
        if (merchantProviderSetting == null)
        {
            return false;
        }

        var result = _dbContext.MerchantProviderSettings.Remove(merchantProviderSetting);

        return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }

    public async Task<MerchantProviderSetting?> GetByIdAsync(Guid id)
    {
        return await _dbContext.MerchantProviderSettings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<MerchantProviderSetting?> GetByMerchantIdAsync(Guid merchantId)
    {
        return await _dbContext.MerchantProviderSettings.FirstOrDefaultAsync(x => x.MerchantId == merchantId);
    }

    public async Task<List<MerchantProviderSetting>> GetAllAsync()
    {
        return await _dbContext.MerchantProviderSettings.ToListAsync();
    }

    public MerchantProviderSetting Update(MerchantProviderSetting merchantProviderSetting)
    {
        var result = _dbContext.MerchantProviderSettings.Update(merchantProviderSetting);

        return result.Entity;
    }

}