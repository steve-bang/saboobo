
using SaBooBo.MerchantService.Domain.Repositories;

namespace MerchantService.Application.Features.Queries;

public class ListBannersByMerchantIdQueryHandler(
    IBannerRepository _bannerRepository
) : IRequestHandler<ListBannersByMerchantIdQuery, List<BannerResponse>>
{

    public async Task<List<BannerResponse>> Handle(ListBannersByMerchantIdQuery request, CancellationToken cancellationToken)
    {
        // Get the banners by the merchant id
        var banners = await _bannerRepository.GetByMerchantIdAsync(request.MerchantId);

        // Order the banners by the order
        banners = banners.OrderBy(banner => banner.Order).ToList();

        // Return the banners
        return banners.Select(banner => new BannerResponse(banner.Id, banner.Name, banner.ImageUrl, banner.Link, banner.Order)).ToList();
    }
}