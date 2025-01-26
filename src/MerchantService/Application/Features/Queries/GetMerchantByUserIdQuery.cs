
using SaBooBo.MerchantService.Domain.AggregatesModel;

namespace MerchantService.Application.Features.Queries;

public record GetMerchantByUserIdQuery(Guid UserId) : IRequest<Merchant>;
