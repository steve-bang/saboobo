
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.AggregatesModel;
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Queries;

public class GetMerchantByUserIdQueryHandler(
    IMerchantRepository _merchantRepository
) : IRequestHandler<GetMerchantByUserIdQuery, Merchant>
{


    public async Task<Merchant> Handle(GetMerchantByUserIdQuery request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.GetByUserIdAsync(request.UserId);

        if (merchant == null)
        {
            throw new NotFoundException(
                "Merchant_Not_Found",
                $"Merchant with UserId: {request.UserId} not found.",
                "The merchant with the specified UserId was not found. Please check the UserId and try again."
            );
        }

        return merchant;
    }
}