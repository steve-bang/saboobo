
namespace SaBooBo.Product.Application.Features.Queries;

public class GetProductByIdQueryHandler(
    IProductRepository ProductRepository
) : IRequestHandler<GetProductByIdQuery, Domain.AggregatesModel.Product?>
{
    public async Task<Domain.AggregatesModel.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(request.Id);

        return product;
    }
}