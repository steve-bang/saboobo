
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Infrastructure.Repositories;

public class UserAddressRepository : IUserAddressRepository
{
    private readonly UserAppContext _context;

    public UserAddressRepository(UserAppContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<UserAddress> CreateAsync(UserAddress userAddress)
    {
        var entity = await _context.UserAddresses.AddAsync(userAddress);

        return entity.Entity;
    }

    public Task<bool> DeleteAsync(UserAddress userAddress)
    {
        var entity = _context.UserAddresses.Remove(userAddress);

        return Task.FromResult(entity.State == EntityState.Deleted);
    }

    public async Task<IEnumerable<UserAddress>> GetAllAsync()
    {
        return await _context.UserAddresses.ToListAsync();
    }

    public async Task<UserAddress?> GetByIdAsync(Guid id)
    {
        return await _context.UserAddresses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UserAddress>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserAddresses.Where(x => x.UserId == userId).ToListAsync() ?? new List<UserAddress>();
    }

    public UserAddress UpdateAsync(UserAddress userAddress)
    {
        var entity = _context.UserAddresses.Update(userAddress);

        return entity.Entity;
    }
}