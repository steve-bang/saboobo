
using MediatR;

namespace SaBooBo.OrderService.Application.Features.Commands;

public record UpdateOrderShippingStatusCommand(
    Guid MerchantId,
    Guid OrderId
) : IRequest<bool>;