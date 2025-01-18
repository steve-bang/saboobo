
namespace SaBooBo.Product.Application.Features.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Domain.AggregatesModel.Product?>;