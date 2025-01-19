
namespace SaBooBo.Product.Application.Features.Queries;

public record ListProductQuery() : IRequest<List<Domain.AggregatesModel.Product>>;