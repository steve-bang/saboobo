
using SaBooBo.UserService.Domain.AggregatesModel;
using SaBooBo.UserService.Domain.Exceptions;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class DeleteUserAddressCommandHandler(
    IUserAddressRepository _userAddressRepository,
    IUserRepository _userRepository
) : IRequestHandler<DeleteUserAddressCommand, bool>
{

    public async Task<bool> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
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

        // Delete the address
        var result = await _userAddressRepository.DeleteAsync(userAddress);

        await _userAddressRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
