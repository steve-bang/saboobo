
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace MerchantService.Application.Features.Commands;

public class DeleteBannerCommandHandler(
    IBannerRepository _bannerRepository,
    IMerchantRepository _merchantRepository
) : IRequestHandler<DeleteBannerCommand, bool>
{

    public async Task<bool> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.GetByIdAsync(request.MerchantId);
        if (merchant == null)
        {
            throw new MerchantNotFoundException(request.MerchantId);
        }

        var banner = await _bannerRepository.GetByIdAsync(request.BannerId);
        if (banner == null)
        {
            throw new NotFoundException(
                "Banner_Not_Found",
                $"Banner with Id: {request.BannerId} not found.",
                "The banner with the specified Id was not found. Please check the Id and try again."
            );
        }

        if (banner.MerchantId != merchant.Id)
        {
            throw new BadRequestException(
                "Merchant_Banner_Not_Match",
                "The merchant and the banner do not match.",
                "The merchant and the banner do not match. Please check the merchant and the banner and try again."
            );
        }

        var result = _bannerRepository.Delete(banner);

        if (!result)
        {
            throw new SaBooBoException(
                "Banner_Deletion_Failed",
                "Failed to delete the banner.",
                "Failed to delete the banner. Please try again."
            );
        }

        await _bannerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}