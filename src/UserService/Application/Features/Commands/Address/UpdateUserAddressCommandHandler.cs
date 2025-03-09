
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class UpdateUserAddressCommandHandler(
    IUserAddressRepository _userAddressRepository,
    IUserRepository _userRepository
) : IRequestHandler<UpdateUserAddressCommand, Guid>
{

    public async Task<Guid> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
    {
        // Checks if the address exists
        var userAddress = await _userAddressRepository.GetByIdAsync(request.Id);

        if (userAddress == null)
        {
            throw new UserAddressNotFoundException();
        }

        // Checks if the user is updating the default address
        var user = await _userRepository.GetByIdAsync(userAddress.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        // Checks if the user is updating the default address
        if (user.Id != userAddress.UserId)
        {
            throw new UserAddressNotFoundException();
        }


        userAddress.Update(
            contactName: request.ContactName,
            phoneNumber: request.PhoneNumber,
            addressLine1: request.AddressLine1,
            addressLine2: request.AddressLine2,
            city: request.City,
            state: request.State,
            country: request.Country,
            isDefault: request.IsDefault
        );

        var addressUpdated = _userAddressRepository.UpdateAsync(userAddress);

        await _userAddressRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return addressUpdated.Id;
    }
}
