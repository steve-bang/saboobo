
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Topping : AggregateRoot
{
    public string Name { get; } = null!;

    public long Price { get; }

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public Topping(string name, long price)
    {
        Id = CreateNewId();

        Name = name;
        Price = price;
    }
}