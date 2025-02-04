
namespace MerchantService.Application.Features.Commands;

public record CreateBannerCommand(
    Guid MerchantId,
    string Name,
    string ImageUrl,
    string Link
) : IRequest<Guid>;