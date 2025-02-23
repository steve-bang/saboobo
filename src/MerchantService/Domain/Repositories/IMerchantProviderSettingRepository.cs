
namespace SaBooBo.MerchantService.Domain.Repositories;

public interface IMerchantProviderSettingRepository : IRepository
{
    Task<MerchantProviderSetting> CreateAsync(MerchantProviderSetting merchantProviderSetting);

    MerchantProviderSetting Update(MerchantProviderSetting merchantProviderSetting);

    Task<MerchantProviderSetting?> GetByIdAsync(Guid id);

    Task<MerchantProviderSetting?> GetByMerchantIdAsync(Guid merchantId);

    Task<List<MerchantProviderSetting>> GetAllAsync();

    bool Delete(MerchantProviderSetting merchantProviderSetting);
}