

using SaBooBo.Product.Domain.AggregatesModel;
using SaBooBo.Product.Domain.Repository;

namespace SaBooBo.Product.Application.Features.Commands;

public class ProductCreateCommandHandler(
    IProductRepository ProductRepository
) : IRequestHandler<ProductCreateCommand, Guid>
{
    public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.AggregatesModel.Product.Create(
            name: request.Name,
            sku: request.Sku,
            price: request.Price,
            description: request.Description,
            urlImage: request.UrlImage,
            toppings: request.Toppings != null ?
                request.Toppings.Select(x => new Topping(x.Name, x.Price)).ToArray() : null
        );

        var productCreated = ProductRepository.CreateAsync(product);

        await ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return product.Id;
    }
}