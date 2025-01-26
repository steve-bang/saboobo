
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.AggregatesModel;
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Commands;

public class UpdateMerchantCommandHandler(
    IMerchantRepository _merchantRepository
) : IRequestHandler<UpdateMerchantCommand, Merchant>
{

    public async Task<Merchant> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.GetByIdAsync(request.Id);

        if (merchant is null)
        {
            throw new NotFoundException(
                "Merchant_Not_Found",
                $"Merchant with Id: {request.Id} not found.",
                "The merchant with the specified Id was not found. Please check the Id and try again."
            );
        }

        merchant.Update(
            request.Name,
            request.Description,
            request.EmailAddress,
            request.PhoneNumber,
            request.Address,
            request.LogoUrl,
            request.CoverUrl,
            request.Website,
            request.OaUrl
        );

        await _merchantRepository.UpdateAsync(merchant);

        await _merchantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return merchant;
    }
}