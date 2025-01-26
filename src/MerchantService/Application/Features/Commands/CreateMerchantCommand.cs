
using SaBooBo.MerchantService.Domain.AggregatesModel;

namespace MerchantService.Application.Features.Commands;

public record CreateMerchantCommand(
    Guid UserId,
    string Name,
    string Description,
    string EmailAddress,
    string PhoneNumber,
    string Address,
    string? LogoUrl,
    string? CoverUrl,
    string? Website,
    string? OAUrl
) : IRequest<Merchant>;
