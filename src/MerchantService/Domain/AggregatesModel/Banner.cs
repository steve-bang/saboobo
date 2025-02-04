
namespace SaBooBo.MerchantService.Domain.AggregatesModel;

public class Banner : AggregateRoot
{
    public Guid MerchantId { get; private set; }

    public string Name { get; private set; } = null!;

    public string ImageUrl { get; private set; } = null!;

    public string? Link { get; private set; }

    public int Order { get; private set; } = 0;

    public DateTime CreatedAt { get; private set; } = DateTime.Now.ToUniversalTime();


    public Banner(Guid merchantId, string name, string imageUrl, string? link, int order)
    {
        Id = CreateNewId();

        MerchantId = merchantId;
        Name = name;
        ImageUrl = imageUrl;
        Link = link;
        Order = order;
    }

    public void Update(string name, string imageUrl, string? link, int order)
    {
        Name = name;
        ImageUrl = imageUrl;
        Link = link;
        Order = order;
    }

    public static Banner Create(Guid merchantId, string name, string imageUrl, string? link, int order)
    {
        return new Banner(merchantId, name, imageUrl, link, order);
    }
}