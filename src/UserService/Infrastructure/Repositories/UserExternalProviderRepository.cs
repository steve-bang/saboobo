
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Infrastructure.Repositories;

public class UserExternalProviderRepository(
    UserAppContext _context
) : IUserExternalProviderRepository
{
    public IUnitOfWork UnitOfWork => _context;

    public async Task<UserExtenalProvider> CreateAsync(UserExtenalProvider userExternalProvider)
    {
        var entity = await _context.UserExternalProviders.AddAsync(userExternalProvider);

        return entity.Entity;
    }

    public Task<bool> DeleteAsync(UserExtenalProvider userExternalProvider)
    {
        var entity = _context.UserExternalProviders.Remove(userExternalProvider);

        return Task.FromResult(entity.State == EntityState.Deleted);
    }

    public async Task<IEnumerable<UserExtenalProvider>> GetAllAsync()
    {
        return await _context.UserExternalProviders.ToListAsync();
    }

    public async Task<UserExtenalProvider?> GetByIdAsync(Guid id)
    {
        return await _context.UserExternalProviders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserExtenalProvider?> GetByUserExternalIdAsync(string userExternalId)
    {
        return await _context.UserExternalProviders.FirstOrDefaultAsync(x => x.UserExternalId == userExternalId);
    }

    public async Task<UserExtenalProvider?> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserExternalProviders.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public UserExtenalProvider UpdateAsync(UserExtenalProvider userExternalProvider)
    {
        var entity = _context.UserExternalProviders.Update(userExternalProvider);

        return entity.Entity;
    }
}