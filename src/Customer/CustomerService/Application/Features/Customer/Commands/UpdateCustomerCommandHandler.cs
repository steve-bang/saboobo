
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.CustomerService.Application.Features.Commands;

public class UpdateCustomerCommandHandler(
    ICustomerRepository _customerRepository
) : IRequestHandler<UpdateCustomerCommand, Customer>
{

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            throw new NotFoundException(
                "Customer_not_found",
                $"Customer with id {request.Id} not found",
                "The customer with the given id was not found, please check the id and try again."
            );
        }

        customer.Update(
            name: request.Name,
            phoneNumber: request.PhoneNumber,
            emailAddress: request.EmailAddress,
            avatarUrl: request.AvatarUrl,
            dateOfBirth: request.DateOfBirth,
            gender: request.Gender
        );

        _customerRepository.Update(customer);

        await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return customer;

    }
}