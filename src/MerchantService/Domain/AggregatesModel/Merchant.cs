
using System.Security.Cryptography.X509Certificates;
using SaBooBo.Domain.Shared;

namespace SaBooBo.MerchantService.Domain.AggregatesModel;

public class Merchant : AggregateRoot
{
    public Guid UserId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public string EmailAddress { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public string? LogoUrl { get; private set; } 

    public string? CoverUrl { get; private set; }

    public string? Website { get; private set; }

    public string? OAUrl { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.Now.ToUniversalTime();

    public Merchant(Guid userId, string name, string description, string emailAddress, string phoneNumber, string address, string? logoUrl, string? coverUrl, string? website, string? oAUrl)
    {
        Id = CreateNewId();

        UserId = userId;
        Name = name;
        Description = description;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Address = address;
        LogoUrl = logoUrl;
        CoverUrl = coverUrl;
        Website = website;
        OAUrl = oAUrl;
    }

    public void Update(string name, string description, string emailAddress, string phoneNumber, string address, string? logoUrl, string? coverUrl, string? website, string? oAUrl)
    {
        Name = name;
        Description = description;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Address = address;
        LogoUrl = logoUrl;
        CoverUrl = coverUrl;
        Website = website;
        OAUrl = oAUrl;
    }
    
    public static Merchant Create(Guid userId, string name, string description, string emailAddress, string phoneNumber, string address, string? logoUrl, string? coverUrl, string? website, string? oAUrl)
    {
        return new(userId, name, description, emailAddress, phoneNumber, address, logoUrl, coverUrl, website, oAUrl);
    }
    
}