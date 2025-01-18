
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Product : AggregateRoot
{
    private List<Topping> _toppings = new();

    public string Name { get; } = null!;

    public string? Sku { get; }

    public long Price { get; }

    public string? Description { get; } 

    public string? UrlImage { get; }

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public IReadOnlyCollection<Topping> Toppings => _toppings.AsReadOnly();

    public Product() {}

    public Product(string name, string? sku, long price, string? description, string? urlImage)
    {
        Id = CreateNewId();
        Name = name;
        Sku = sku;
        Price = price;
        Description = description;
        UrlImage = urlImage;
    }

    public Product(string name, string? sku, long price, string? description, string? urlImage, Topping[]? toppings)
        : this(name, sku, price, description, urlImage)
    {
        if(toppings != null && toppings.Any())
        {
            _toppings.AddRange(toppings);
        }
    }

    public static Product Create(string name, string? sku, long price, string? description, string? urlImage, Topping[]? toppings)
    {
        return new(name, sku, price, description, urlImage, toppings);
    }
}