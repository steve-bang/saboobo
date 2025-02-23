

namespace SaBooBo.CartService.Application.Features.Commands;

public record AddItemToCartCommand(
    Guid CartId,
    CartItemsCommandRequest[] Items
) : IRequest<Cart>;