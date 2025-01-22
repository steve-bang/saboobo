
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Commands;

public class CreateProductCommandHandler(
    IProductRepository ProductRepository
) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.AggregatesModel.Product.Create(
            merchantId: request.MerchantId,
            categoryId: request.CategoryId,
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