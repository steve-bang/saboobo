

using SaBooBo.MerchantService.Apis.Requests;

namespace MerchantService.Application.Features.Commands;

public record UpdateBannersCommand(Guid MerchantId, List<BannersUpdateRequest> Banners) : IRequest<bool>;