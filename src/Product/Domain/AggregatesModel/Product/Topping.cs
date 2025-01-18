
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Topping : AggregateRoot
{
    public string Name { get; } = null!;

    public long Price { get; }

    public DateTime CreatedDate { get; }

    public Topping(string name, long price)
    {
        Id = CreateNewId();

        Name = name;
        Price = price;

        CreatedDate = DateTime.Now;
    }
}