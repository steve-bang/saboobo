
namespace SaBooBo.CartService.Application.Features.Commands;

public record UpdateItemCartCommand(
    Guid CartId,
    CartItemsCommandRequest[] Items
) : IRequest<Cart>;