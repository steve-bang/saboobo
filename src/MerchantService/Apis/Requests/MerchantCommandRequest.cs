
namespace SaBooBo.MerchantService.Apis.Requests;

public record MerchantCommandRequest(
    string Name,
    string Code,
    string Description,
    string EmailAddress,
    string PhoneNumber,
    string Address,
    string? LogoUrl,
    string? CoverUrl,
    string? Website,
    string? OaUrl
);