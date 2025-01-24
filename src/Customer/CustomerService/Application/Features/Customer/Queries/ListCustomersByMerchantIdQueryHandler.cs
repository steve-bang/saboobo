

namespace SaBooBo.CustomerService.Application.Features.Queries;

public class ListCustomersByMerchantIdQueryHandler(
    ICustomerRepository _customerRepositiry
) : IRequestHandler<ListCustomersByMerchantIdQuery, List<Customer>>
{
    public async Task<List<Customer>> Handle(ListCustomersByMerchantIdQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepositiry.ListCustomersByMerchantId(request.MerchantId);

        return customers;
    }
}