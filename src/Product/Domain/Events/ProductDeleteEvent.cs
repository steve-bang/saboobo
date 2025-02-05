

namespace SaBooBo.Product.Domain.Events;

/// <summary>
/// This event is raised when a product is deleted.
/// </summary>
public class ProductDeleteEvent : IDomainEvent
{
    public Guid ProductId { get; }

    public Guid CategoryId { get; }

    public ProductDeleteEvent(Guid productId, Guid categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    public static ProductDeleteEvent Create(Guid productId, Guid categoryId)
    {
        return new (productId, categoryId);
    }
}