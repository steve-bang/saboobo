
namespace MerchantService.Application.Features.Queries;

public record GetMerchantByIdQuery(Guid Id) : IRequest<Merchant>;