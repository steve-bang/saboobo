
using SaBooBo.Domain.Shared;

namespace SaBooBo.OrderService.Domain.AggregatesModel;

public class ShippingAddress : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = null!;

    public string? Email { get; private set; }

    /// <summary>
    /// The address of the shipping address.
    /// </summary>
    public string AddressDetail { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();


    /// <summary>
    /// Create a new shipping address.
    /// </summary>
    /// <param name="name">The name of the shipping address.</param>
    /// <param name="phoneNumber">The phone number of the shipping address.</param>
    /// <param name="addressDetail">The address of the shipping address. The address can be a street address, apartment number, or P.O. Box.</param>
    public ShippingAddress(string name, string phoneNumber, string addressDetail)
    {
        Id = Guid.NewGuid();

        Name = name;
        PhoneNumber = phoneNumber;
        AddressDetail = addressDetail;
    }

    public ShippingAddress(string name, string phoneNumber, string? email, string addressDetail)
    {
        Id = Guid.NewGuid();

        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        AddressDetail = addressDetail;
    }



    public static ShippingAddress Create(string name, string phoneNumber, string? email, string addressDetail)
    {
        return new ShippingAddress(name, phoneNumber, email, addressDetail);
    }



    public void Update(string name, string phoneNumber, string email, string addressDetail)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        AddressDetail = addressDetail;

        UpdatedAt = DateTime.UtcNow.ToUniversalTime();
    }

}