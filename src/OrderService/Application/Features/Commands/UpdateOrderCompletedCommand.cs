
using MediatR;

namespace SaBooBo.OrderService.Application.Features.Commands;

public record UpdateOrderCompletedCommand(
    Guid MerchantId,
    Guid OrderId
) : IRequest<bool>;
