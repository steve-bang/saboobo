
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.CustomerService.Application.Features.Queries;

public class GetCustomerByPhoneNumberQueryHandler(
    ICustomerRepository _customerRepository
) : IRequestHandler<GetCustomerByPhoneNumberQuery, Customer>
{

    public async Task<Customer> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        if (customer is null)
        {
            throw new NotFoundException(
                "Customer_Not_Found",
                $"Customer with phone number {request.PhoneNumber} not found.",
                "The customer with the provided phone number was not found. Please check the phone number and try again."
            );
        }

        return customer;
    }
}