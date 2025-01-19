
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Queries;

public record ListCategoryQuery(Guid MerchantId) : IRequest<List<Category>>;