
using SaBooBo.Domain.Shared;
using SaBooBo.MerchantService.Domain.AggregatesModel;

namespace SaBooBo.MerchantService.Domain.Repositories;

public interface IBannerRepository : IRepository
{
    Task<Banner> CreateAsync(Banner banner);

    Banner Update(Banner banner);

    Task<Banner?> GetByIdAsync(Guid id);

    Task<List<Banner>> GetByMerchantIdAsync(Guid merchantId);

    Task<int> CountByMerchantId(Guid merchantId);

    bool Delete(Banner banner);
}