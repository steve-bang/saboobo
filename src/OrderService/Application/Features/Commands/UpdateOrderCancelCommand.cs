
using MediatR;

namespace SaBooBo.OrderService.Application.Features.Commands;

public record UpdateOrderCancelCommand(
    Guid MerchantId,
    Guid OrderId
) : IRequest<bool>;
