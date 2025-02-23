
namespace SaBooBo.CartService.Application.Features.Commands;

public record UpdateCartCommand(
    Guid CartId,
    Guid ProductId,
    int Quantity,
    string Notes
) : IRequest<Cart>;