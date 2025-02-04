
namespace MerchantService.Application.Features.Queries;

public record ListBannersByMerchantIdQuery(Guid MerchantId) : IRequest<List<BannerResponse>>;

public class BannerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? Link { get; set; }
    public int Order { get; set; }

    public BannerResponse(Guid id, string name, string imageUrl, string? link, int order)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Link = link;
        Order = order;
    }
}