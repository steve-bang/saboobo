
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Infrastructure.Repositories;

public class UserRepository(
    UserAppContext _context
) : IUserReposiroty
{
    public IUnitOfWork UnitOfWork => _context;

    public async Task<User> CreateAsync(User user)
    {
        var entity = await _context.Users.AddAsync(user);

        return entity.Entity;
    }

    public Task<bool> DeleteAsync(User user)
    {
        var entity = _context.Users.Remove(user);

        return Task.FromResult(entity.State == EntityState.Deleted);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone);
    }

    public User UpdateAsync(User user)
    {
        var entity = _context.Users.Update(user);

        return entity.Entity;
    }
}