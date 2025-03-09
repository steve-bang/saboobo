
using MediatR;

namespace SaBooBo.OrderService.Application.Features.Commands;

public record UpdateOrderShippingTotalToConfirmCommand(
    Guid MerchantId,
    Guid OrderId,
    decimal ShippingTotal,
    bool IsFreeShipping
) : IRequest<bool>;
    

