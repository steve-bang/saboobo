
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Commands;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Code,
    string? Description,
    string? IconUrl
) : IRequest<Category>;