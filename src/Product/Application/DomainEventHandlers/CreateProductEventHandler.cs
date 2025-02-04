
using SaBooBo.Product.Domain.Events;

namespace SaBooBo.Product.Application.DomainEventHandlers;

public class CreateProductEventHandler : INotificationHandler<ProductCreateEvent>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductEventHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(notification.Product.CategoryId);

        if (category == null)
        {
            Console.WriteLine("Category not found");
            return;
        }

        category.IncrementProduct();

        _categoryRepository.Update(category);

        await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}