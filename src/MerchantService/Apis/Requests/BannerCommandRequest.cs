namespace SaBooBo.MerchantService.Apis.Requests;

public record BannerCommandRequest(
    string Name,
    string ImageUrl,
    string Link
);

public record BannersUpdateRequest(
    Guid Id,
    string Name,
    string ImageUrl,
    string Link
);