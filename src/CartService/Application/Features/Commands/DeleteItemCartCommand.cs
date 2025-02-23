
namespace SaBooBo.CartService.Application.Features.Commands;

public record DeleteItemCartCommand(
    Guid CartId,
    Guid[] CartItemIds
) : IRequest<Cart>;