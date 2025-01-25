
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Domain.Repositories;

public interface IUserReposiroty : IRepository
{
    Task<User?> GetAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneAsync(string phone);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    User UpdateAsync(User user);
    Task<bool> DeleteAsync(User user);
}