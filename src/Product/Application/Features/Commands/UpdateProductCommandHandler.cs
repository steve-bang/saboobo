

using Microsoft.EntityFrameworkCore;
using SaBooBo.Product.Domain.AggregatesModel;
using SaBooBo.Product.Infrastructure;

namespace SaBooBo.Product.Application.Features.Commands;

public class UpdateProductCommandHandler(
    IProductRepository ProductRepository,
    IToppingRepository ToppingRepository,
    ProductAppContext ProductAppContext
) : IRequestHandler<UpdateProductCommand, Domain.AggregatesModel.Product>
{
    public async Task<Domain.AggregatesModel.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Find product
        var product = await ProductRepository.GetByIdAsync(request.Id);

        if (product is null)             
            throw new NotFoundException(
                ErrorCodes.CategoryNotFound,
                "The category was not found.",
                "The category was not found in the system, please check your id request."
            );

        // 2. Update product
        product.Update(
            name: request.Name,
            sku: request.Sku,
            price: request.Price,
            description: request.Description,
            urlImage: request.UrlImage
        );


        // 3. Update topping
        // 3.1 If toppings request is has item
        if (request.Toppings != null && request.Toppings.Any())
        {
            // 3.1.1 Find topping unused in topping request and remove them
            var toppingsHasId = request.Toppings.Where(x => x.Id.HasValue)
                                                .Select(x => x.Id.Value) // Get the Ids of toppings that have an Id
                                                .ToHashSet(); // Use HashSet for O(1) lookups

            // Find the toppings in the product that are not in the request's list of toppings
            var toppingsUnused = product.Toppings
                                        .Where(toppingInProduct => !toppingsHasId.Contains(toppingInProduct.Id))
                                        .ToList(); // Materialize the result

            // Remove unused toppings
            foreach (var topping in toppingsUnused)
            {
                product.RemoveTopping(topping);
            }


            // 3.1.2 Loop for each topping and add or update.
            foreach (var topping in request.Toppings)
            {
                if (topping.Id is null)
                {
                    var newTopping = new Topping(topping.Name, topping.Price);
                    product.AddTopping(newTopping);

                    // Make is new
                    ProductAppContext.Entry(newTopping).State = EntityState.Added;
                }
                else
                {
                    product.UpdateTopping(topping.Id.Value, topping.Name, topping.Price);
                }
            }
        }
        // 3.2 If request topping has no item, delet all
        else
        {
            await ToppingRepository.DeleteByProductIdAsync(request.Id);
        }

        ProductRepository.UpdateByIdAsync(product);

        await ToppingRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return product;

    }
}