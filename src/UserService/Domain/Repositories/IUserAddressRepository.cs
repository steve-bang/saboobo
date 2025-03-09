
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Domain.Repositories;

public interface IUserAddressRepository : IRepository
{
    Task<UserAddress?> GetByIdAsync(Guid id);
    Task<List<UserAddress>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserAddress>> GetAllAsync();
    Task<UserAddress> CreateAsync(UserAddress userAddress);
    UserAddress UpdateAsync(UserAddress userAddress);
    Task<bool> DeleteAsync(UserAddress userAddress);
}