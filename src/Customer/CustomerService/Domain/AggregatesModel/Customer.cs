
namespace SaBooBo.CustomerService.Domain.AggregatesModel;

public class Customer : AggregateRoot
{

    public Guid MerchantId { get; private set; }

    public string Name { get; private set; }

    public string? EmailAddress { get; private set; }

    public string PhoneNumber { get; private set; } = null!;

    public string? AvatarUrl { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public bool IsActive { get; private set; } = false;

    public Gender? Gender { get; private set; }

    public DateTime CreatedDate { get; private set; } = DateTime.Now.ToUniversalTime();

    public Customer(
        Guid merchantId,
        string name,
        string? emailAddress,
        string phoneNumber,
        string? avatarUrl,
        DateTime? dateOfBirth,
        Gender? gender
    )
    {
        Id = CreateNewId();
        
        MerchantId = merchantId;
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        AvatarUrl = avatarUrl;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }

    public void Update(
        string name,
        string emailAddress,
        string phoneNumber,
        string avatarUrl,
        DateTime? dateOfBirth,
        bool isActive,
        Gender gender
    )
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        AvatarUrl = avatarUrl;
        DateOfBirth = dateOfBirth;
        IsActive = isActive;
        Gender = gender;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }


    public static Customer Create(
        Guid merchantId,
        string name,
        string? emailAddress,
        string phoneNumber,
        string? avatarUrl,
        DateTime? dateOfBirth,
        Gender? gender
    )
    {
        return new(merchantId, name, emailAddress, phoneNumber, avatarUrl, dateOfBirth, gender);
    }

}