
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class CreateUserAddressCommandHandler(
    IUserAddressRepository _userAddressRepository
) : IRequestHandler<CreateUserAddressCommand, Guid>
{

    public async Task<Guid> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var userAddress = UserAddress.Create(
            userId: request.UserId,
            contactName: request.ContactName,
            phoneNumber: request.PhoneNumber,
            addressLine1: request.AddressLine1,
            addressLine2: request.AddressLine2,
            city: request.City,
            state: request.State,
            country: request.Country,
            isDefault: request.IsDefault
        );

        var addressCreated = await _userAddressRepository.CreateAsync(userAddress);

        await _userAddressRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return addressCreated.Id;
    }
}