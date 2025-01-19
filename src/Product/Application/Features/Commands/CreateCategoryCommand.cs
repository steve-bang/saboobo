
namespace SaBooBo.Product.Application.Features.Commands;

public record CreateCategoryCommand(
    Guid MerchantId,
    string Name,
    string? Code,
    string? Description,
    string? IconUrl
) : IRequest<Guid>;