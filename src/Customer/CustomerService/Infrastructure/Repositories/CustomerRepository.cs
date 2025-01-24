
namespace SaBooBo.CustomerService.Infrastructure.Repositories;

public class CustomerRepository(CustomerAppContext _dbContext) : ICustomerRepository
{
    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<Customer> CreateAsync(Customer customer)
    {
        var result = await _dbContext.Customers.AddAsync(customer);

        return result.Entity;
    }

    public Customer Update(Customer customer)
    {
        return _dbContext
            .Customers
            .Update(customer)
            .Entity;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _dbContext
            .Customers
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<List<Customer>> GetAllByMerchantIdAsync(Guid merchantId)
    {
        return _dbContext
            .Customers
            .Where(c => c.MerchantId == merchantId)
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await GetByIdAsync(id);

        if (customer is null) return;

        _dbContext.Customers.Remove(customer);
    }

    public Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return _dbContext
            .Customers
            .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
    }

    public Task<Customer?> GetByEmailAsync(string emailAddress)
    {
        return _dbContext
            .Customers
            .FirstOrDefaultAsync(c => c.EmailAddress == emailAddress);
    }

    public Task<List<Customer>> ListCustomersByMerchantId(Guid merchantId)
    {
        return _dbContext
            .Customers
            .Where(c => c.MerchantId == merchantId)
            .ToListAsync();
    }
}