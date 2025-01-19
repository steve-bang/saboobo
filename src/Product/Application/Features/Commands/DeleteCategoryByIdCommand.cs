
namespace SaBooBo.Product.Application.Features.Commands;

public record DeleteCategoryByIdCommand(Guid Id) : IRequest<bool>;