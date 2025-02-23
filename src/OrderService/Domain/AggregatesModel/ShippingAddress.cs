
using SaBooBo.Domain.Shared;

namespace SaBooBo.OrderService.Domain.AggregatesModel;

public class ShippingAddress : AggregateRoot
{
    public string Name { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = null!;

    public string? Email { get; private set; }

    public string? Address { get; private set; }

    public string? City { get; private set; }

    public string? State { get; private set; }

    public string? Country { get; private set; }

    public string? ZipCode { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public ShippingAddress(string name, string address, string city, string state, string country, string zipCode, string phoneNumber, string email)
    {
        Id = Guid.NewGuid();

        Name = name;
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void Update(string name, string address, string city, string state, string country, string zipCode, string phoneNumber, string email)
    {
        Name = name;
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
        Email = email;
    }

}