
namespace SaBooBo.CustomerService.Application.Features.Queries;

public record GetCustomerByPhoneNumberQuery(string PhoneNumber) : IRequest<Customer>;