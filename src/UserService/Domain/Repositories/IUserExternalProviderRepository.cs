
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Domain.Repositories;

public interface IUserExternalProviderRepository : IRepository
{
    Task<UserExtenalProvider?> GetByIdAsync(Guid id);
    Task<UserExtenalProvider?> GetByUserExternalIdAsync(string userExternalId);
    Task<UserExtenalProvider?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserExtenalProvider>> GetAllAsync();
    Task<UserExtenalProvider> CreateAsync(UserExtenalProvider userExtenalProvider);
    UserExtenalProvider UpdateAsync(UserExtenalProvider userExtenalProvider);
    Task<bool> DeleteAsync(UserExtenalProvider userExtenalProvider);
}