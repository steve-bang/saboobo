
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MerchantService.Domain.Errors;
using SaBooBo.MerchantService.Domain.Exceptions;
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Commands;

public class UpdateBannersCommandHandler(
    IMerchantRepository _merchantRepository,
    IBannerRepository _bannerRepository
) : IRequestHandler<UpdateBannersCommand, bool>
{

    public async Task<bool> Handle(UpdateBannersCommand request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.GetByIdAsync(request.MerchantId);
        
        if (merchant == null)
        {
            throw new MerchantNotFoundException(request.MerchantId);
        }

        var banners = request.Banners;

        int newOrderBanner = 1;

        foreach (var banner in banners)
        {
            var bannerEntity = await _bannerRepository.GetByIdAsync(banner.Id);
            if (bannerEntity == null)
            {
                throw new NotFoundException(
                    BannerErrors.NotFound,
                    $"Banner with Id: {banner.Id} not found.",
                    "The banner with the specified Id was not found. Please check the Id and try again."
                );
            }

            if (bannerEntity.MerchantId != merchant.Id)
            {
                throw new BadRequestException(
                    BannerErrors.MerchantBannerNotMatch,
                    "The merchant and the banner do not match.",
                    "The merchant and the banner do not match. Please check the merchant and the banner and try again."
                );
            }

            bannerEntity.Update(
                banner.Name,
                banner.ImageUrl,
                banner.Link,
                newOrderBanner++
            );

            _bannerRepository.Update(bannerEntity);
        }

        await _bannerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}