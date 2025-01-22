
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Product : AggregateRoot
{
    private List<Topping> _toppings = new();

    public Guid CategoryId { get; private set; }

    public Guid MerchantId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Sku { get; private set; }

    public long Price { get; private set; }

    public string? Description { get; private set; }

    public string? UrlImage { get; private set; }

    public int TotalPurchased { get; private set; } = 0;

    public IReadOnlyCollection<Topping> Toppings => _toppings.AsReadOnly();

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public Product() { }

    public Product(string name, string? sku, long price, string? description, string? urlImage)
    {
        Id = CreateNewId();
        Name = name;
        Sku = sku;
        Price = price;
        Description = description;
        UrlImage = urlImage;
    }

    public Product(
        Guid merchantId,
        Guid categoryId,
        string name,
        string? sku,
        long price,
        string? description,
        string? urlImage
    ) : this(name, sku, price, description, urlImage)
    {
        MerchantId = merchantId;
        CategoryId = categoryId;
    }

    public Product(string name, string? sku, long price, string? description, string? urlImage, Topping[]? toppings)
        : this(name, sku, price, description, urlImage)
    {
        if (toppings != null && toppings.Any())
        {
            _toppings.AddRange(toppings);
        }
    }

    public Product(
        Guid merchantId,
        Guid categoryId,
        string name,
        string? sku,
        long price,
        string? description,
        string? urlImage,
        Topping[]? toppings
    )
    : this(name, sku, price, description, urlImage, toppings)
    {
        MerchantId = merchantId;
        CategoryId = categoryId;
    }

    public static Product Create(
        Guid merchantId,
        Guid categoryId,
        string name,
        string? sku,
        long price,
        string? description,
        string? urlImage,
        Topping[]? toppings
    )
    {
        return new(merchantId, categoryId, name, sku, price, description, urlImage, toppings);
    }

    public static Product Create(
        string name, 
        string? sku, 
        long price, 
        string? description, 
        string? urlImage, 
        Topping[]? toppings
    )
    {
        return new(name, sku, price, description, urlImage, toppings);
    }

    public void Update(string name, string? sku, long price, string? description, string? urlImage)
    {
        Name = name;
        Sku = sku;
        Price = price;
        Description = description;
        UrlImage = urlImage;
    }

    public void AddTopping(Topping topping)
    {
        _toppings.Add(topping);
    }

    public void UpdateTopping(Guid toppingId, string name, long price)
    {
        var topping = _toppings.Find(x => x.Id == toppingId);
        if (topping is null) return;
        topping.Update(name, price);
    }

    public void RemoveTopping(Topping topping)
    {
        _toppings.Remove(topping);
    }

    public void Purchased()
    {
        TotalPurchased += 1;
    }
}