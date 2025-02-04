
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Commands;

public class CreateBannerCommandHandler(
    IBannerRepository _bannerRepository
) : IRequestHandler<CreateBannerCommand, Guid>
{

    public async Task<Guid> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        // Count the number of banners for the merchant
        var count = await _bannerRepository.CountByMerchantId(request.MerchantId);

        int newOrderBanner = count + 1;

        // Create a new banner
        var banner = Banner.Create(request.MerchantId, request.Name, request.ImageUrl, request.Link, newOrderBanner);

        // Save the banner
        await _bannerRepository.CreateAsync(banner);

        await _bannerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return banner.Id;
    }
}