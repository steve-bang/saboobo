
namespace MerchantService.Application.Features.Commands;

public record DeleteBannerCommand(Guid MerchantId, Guid BannerId) : IRequest<bool>;