
namespace SaBooBo.Product.Domain.AggregatesModel;

public class Category : AggregateRoot
{
    public Guid MerchantId { get; private set; }
    
    public string? Code { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public string? IconUrl { get; private set; }

    public int TotalProduct { get; private set; }

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public Category(Guid merchantId, string? code, string name, string? description, string? iconUrl)
    {
        Id = CreateNewId();

        MerchantId = merchantId;
        Code = code;
        Name = name;
        Description = description;
        IconUrl = iconUrl;
        TotalProduct = 0;
    }

    public static Category Create(Guid merchantId, string? code, string name, string? description, string? iconUrl)
    {
        return new(merchantId,code, name, description, iconUrl);
    }

    public void Update(string? code, string name, string? description, string? iconUrl)
    {
        Code = code;
        Name = name;
        Description = description;
        IconUrl = iconUrl;
    }

    public void IncrementProduct()
    {
        TotalProduct += 1;
    }

    public void DecrementProduct()
    {
        if (TotalProduct > 0)
            TotalProduct -= 1;
    }

    public void ResetTotalProduct()
    {
        TotalProduct = 0;
    }
}