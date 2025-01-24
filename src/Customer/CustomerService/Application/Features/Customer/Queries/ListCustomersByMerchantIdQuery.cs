
namespace SaBooBo.CustomerService.Application.Features.Queries;

public record ListCustomersByMerchantIdQuery(Guid MerchantId) : IRequest<List<Customer>>;