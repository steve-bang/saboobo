
namespace SaBooBo.CartService.Application.Features.Commands;

public record PlaceOrderCartCommand(
    Guid CartId,
    ShippingAddress ShippingAddress,
    string? PaymentMethod,
    string? Note,
    DateTime EstimatedDeliveryDateFrom,
    DateTime EstimatedDeliveryDateTo
) : IRequest<bool>;

public class ShippingAddress
{
    public string PhoneNumber { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? AddressDetail { get; private set; }

    public ShippingAddress(string name, string addressDetail, string phoneNumber)
    {
        Name = name;
        AddressDetail = addressDetail;
        PhoneNumber = phoneNumber;
    }
}