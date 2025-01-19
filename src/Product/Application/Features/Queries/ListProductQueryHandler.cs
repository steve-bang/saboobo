

namespace SaBooBo.Product.Application.Features.Queries;

public class ListProductQueryHandler(
    IProductRepository ProductRepository
): IRequestHandler<ListProductQuery, List<Domain.AggregatesModel.Product>>
{
    public async Task<List<Domain.AggregatesModel.Product>> Handle(ListProductQuery request, CancellationToken cancellationToken)
    {
        var products = await ProductRepository.ListAllAsync();

        return products;
    }
}