
using SaBooBo.Product.Domain.Filters;

namespace SaBooBo.Product.Application.Features.Queries;

public record ListProductQuery(ProductFilter Filter) : IRequest<List<Domain.AggregatesModel.Product>>;