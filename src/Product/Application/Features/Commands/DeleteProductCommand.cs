
namespace SaBooBo.Product.Application.Features.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;