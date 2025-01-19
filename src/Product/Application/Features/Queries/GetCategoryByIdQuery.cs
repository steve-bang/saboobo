
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Category?>;