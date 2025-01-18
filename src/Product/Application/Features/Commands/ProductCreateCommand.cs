
namespace SaBooBo.Product.Application.Features.Commands;

public record ProductCreateCommand(
    string? Sku,
    string Name,
    long Price,
    string? Description,
    string? UrlImage,
    ToppingCreate[] Toppings
) : IRequest<Guid>;

public record ToppingCreate(
    string Name,
    long Price
);