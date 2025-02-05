
using SaBooBo.Product.Domain.Events;

namespace SaBooBo.Product.Application.DomainEventHandlers;

public class ProductDeleteEventHandler(
    IProductRepository ProductRepository,
    ICategoryRepository CategoryRepository
) : INotificationHandler<ProductDeleteEvent>
{
    public async Task Handle(ProductDeleteEvent notification, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(notification.ProductId);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        var category = await CategoryRepository.GetByIdAsync(product.CategoryId);

        if (category == null)
        {
            Console.WriteLine("Category not found");
            return;
        }

        category.DecrementProduct();

        CategoryRepository.Update(category);

        await CategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

}