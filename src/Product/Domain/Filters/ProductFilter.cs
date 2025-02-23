
namespace SaBooBo.Product.Domain.Filters;
public class ProductFilter
{
    public Guid MerchantId { get; set; }
    public string? Keyword { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public Guid? CategoryId { get; set; }
}