

namespace SaBooBo.MerchantService.Domain.Repositories;

public interface IMerchantRepository : IRepository
{
    Task<Merchant> CreateAsync(Merchant merchant);

    Task<Merchant> UpdateAsync(Merchant merchant);

    Task<Merchant?> GetByIdAsync(Guid id);

    Task<Merchant?> GetByCodeAsync(string code);

    Task<Merchant?> GetByUserIdAsync(Guid userId);

    bool Delete(Merchant merchant);

    Task<List<Merchant>> GetAllAsync();
}