
namespace SaBooBo.CustomerService.Domain.Repositories;

public interface ICustomerRepository : IRepository
{
    Task<Customer> CreateAsync(Customer customer);

    Customer Update(Customer customer);

    Task<Customer> GetByIdAsync(Guid id);

    Task<List<Customer>> GetAllByMerchantIdAsync(Guid merchantId);

    Task DeleteAsync(Guid id);

    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);

    Task<Customer?> GetByEmailAsync(string email);

    Task<List<Customer>> ListCustomersByMerchantId(Guid merchantId);
}