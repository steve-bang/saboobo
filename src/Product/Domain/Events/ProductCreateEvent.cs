

namespace SaBooBo.Product.Domain.Events;

/// <summary>
/// This event is raised when a product is created.
/// </summary>
public class ProductCreateEvent : IDomainEvent
{
    public AggregatesModel.Product Product { get; }

    public ProductCreateEvent(AggregatesModel.Product product)
    {
        Product = product;
    }

    public static ProductCreateEvent Create(AggregatesModel.Product product)
    {
        return new ProductCreateEvent(product);
    }
}