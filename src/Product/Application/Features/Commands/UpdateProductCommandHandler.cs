

using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Application.Features.Commands;

public class UpdateProductCommandHandler(
    IProductRepository ProductRepository,
    IToppingRepository ToppingRepository
) : IRequestHandler<UpdateProductCommand, Domain.AggregatesModel.Product>
{
    public async Task<Domain.AggregatesModel.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(request.Id);

        if (product is null) throw new ArgumentNullException(nameof(product));

        product.Update(
            name: request.Name,
            sku: request.Sku,
            price: request.Price,
            description: request.Description,
            urlImage: request.UrlImage
        );

        if (request.Toppings != null && request.Toppings.Any())
        {
            foreach (var topping in request.Toppings)
            {

                if (topping.Id is null)
                {
                    product.AddTopping(new Topping(topping.Name, topping.Price));
                }
                else
                {
                    product.UpdateTopping(topping.Id.Value, topping.Name, topping.Price);
                }

            }
        }
        else
        {
            await ToppingRepository.DeleteByProductIdAsync(request.Id);
        }

        await ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        await ToppingRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return product;

    }
}