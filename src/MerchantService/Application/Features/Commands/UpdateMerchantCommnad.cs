

namespace MerchantService.Application.Features.Commands;

public record UpdateMerchantCommand(
    Guid Id,
    string Name,
    string Description,
    string EmailAddress,
    string PhoneNumber,
    string Address,
    string? LogoUrl,
    string? CoverUrl,
    string? Website,
    string? OaUrl
) : IRequest<Merchant>;