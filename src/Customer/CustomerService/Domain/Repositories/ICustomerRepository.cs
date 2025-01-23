
namespace SaBooBo.CustomerService.Domain.Repositories;

public interface ICustomerRepository : IRepository
{
    Task<Customer> CreateAsync(Customer customer);

    Customer Update(Customer customer);

    Task<Customer> GetAsync(Guid id);

    Task<List<Customer>> GetAllByMerchantIdAsync(Guid merchantId);

    Task DeleteAsync(Guid id);

    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);

    Task<Customer?> GetByEmailAsync(string email);
}