
namespace SaBooBo.CustomerService.Application.Features.Commands;

public class CreateCustomerCommandHandler(
    ICustomerRepository _customerRepository
) : IRequestHandler<CreateCustomerCommand, Guid>
{

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            merchantId: request.MerchantId,
            name: request.Name,
            phoneNumber: request.PhoneNumber,
            emailAddress: request.EmailAddress,
            avatarUrl: request.AvatarUrl,
            dateOfBirth: request.DateOfBirth,
            gender: request.Gender
        );

        await _customerRepository.CreateAsync(customer);

        await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return customer.Id;

    }
}