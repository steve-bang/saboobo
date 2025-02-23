
using SaBooBo.Domain.Shared.ExceptionHandler;

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
            throw new MerchantNotFoundException(request.Id);
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