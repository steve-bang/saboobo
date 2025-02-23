
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.UserService.Application.Clients;
using SaBooBo.UserService.Domain.Repositories;

namespace SaBooBo.UserService.Application.Features.Commands;

public class CheckPhoneNumberUserQueryHandler(
    IUserRepository _userRepository,
    IMerchantClient _merchantClient
) : IRequestHandler<CheckPhoneNumberUserQuery, bool>
{


    public async Task<bool> Handle(CheckPhoneNumberUserQuery request, CancellationToken cancellationToken)
    {
        if (request.MerchantId.HasValue)
        {

            var merchant = await _merchantClient.GetMerchantByIdAsync(request.MerchantId.Value);

            if (merchant is null)
            {
                throw new NotFoundException(
                    "Merchant_Not_Found",
                    $"Merchant with id {request.MerchantId} not found",
                    "The merchant you are trying to register with does not exist, please check the merchant id and try again."
                );
            }
        }

        var user = await _userRepository.GetByPhoneAsync(request.PhoneNumber);

        if (user == null)
        {
            throw new NotFoundException(
                "User_not_found",
                $"User with phone number {request.PhoneNumber} not found",
                "The user with the specified phoneNumber was not found"
            );
        }

        return user != null;
    }
}