
namespace SaBooBo.Product.Application.Features.Commands;

public record UpdateProductCommand(
    Guid Id,
    string? Sku,
    string Name,
    long Price,
    string? Description,
    string? UrlImage,
    ToppingUpdateRequest[]? Toppings
) : IRequest<Domain.AggregatesModel.Product>;

public record ToppingUpdateRequest( Guid? Id ) : ToppingRequest;