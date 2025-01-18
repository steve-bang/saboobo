
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Topping : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public long Price { get; private set; }

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public Topping(string name, long price)
    {
        Id = CreateNewId();

        Name = name;
        Price = price;
    }

    public void Update(string name, long price)
    {
        Name = name;
        Price = price;
    }
}