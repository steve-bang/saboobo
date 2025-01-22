
namespace SaBooBo.Product.Application.Features.Commands;

public record CreateProductCommand(
    Guid MerchantId,
    Guid CategoryId,
    string? Sku,
    string Name,
    long Price,
    string? Description,
    string? UrlImage,
    ToppingRequest[] Toppings
) : IRequest<Guid>;

public record ToppingRequest
{
    public string Name { get; set; } = null!;

    public long Price { get; set; }
}