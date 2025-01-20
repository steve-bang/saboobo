
namespace SaBooBo.Product.Application.Features.Queries;

public class GetProductByIdQueryHandler(
    IProductRepository ProductRepository
) : IRequestHandler<GetProductByIdQuery, Domain.AggregatesModel.Product?>
{
    public async Task<Domain.AggregatesModel.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(request.Id);

        if (product is null) throw new NotFoundException(
            ErrorCodes.CategoryNotFound,
            "The product was not found.",
            "The product was not found in the system, please check your id request."
        );

        return product;
    }
}