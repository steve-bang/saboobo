
using SaBooBo.MerchantService.Domain.Repositories;

namespace SaBooBo.MerchantService.Infrastructure.Repositories;

public class BannerRepository : IBannerRepository
{
    private readonly MerchantAppContext _dbContext;

    public BannerRepository(MerchantAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<Banner> CreateAsync(Banner banner)
    {
        var result = await _dbContext.Banners.AddAsync(banner);

        return result.Entity;
    }

    public bool Delete(Banner banner)
    {
        if (banner == null)
        {
            return false;
        }

        var result = _dbContext.Banners.Remove(banner);

        return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }

    public async Task<Banner?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Banners.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Banner>> GetByMerchantIdAsync(Guid merchantId)
    {
        return await _dbContext.Banners.Where(x => x.MerchantId == merchantId).ToListAsync();
    }

    public async Task<int> CountByMerchantId(Guid merchantId)
    {
        return await _dbContext.Banners.CountAsync(x => x.MerchantId == merchantId);
    }

    public Banner Update(Banner banner)
    {
        var result = _dbContext.Banners.Update(banner);

        return result.Entity;
    }
    
}