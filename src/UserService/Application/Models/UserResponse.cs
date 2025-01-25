
namespace SaBooBo.UserService.Application.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserResponse(Guid id, string phoneNumber, string name, string? emailAddress, string? avatarUrl, bool isActive, DateTime? lastLoginAt, DateTime createdAt)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Name = name;
        EmailAddress = emailAddress;
        AvatarUrl = avatarUrl;
        IsActive = isActive;
        LastLoginAt = lastLoginAt;
        CreatedAt = createdAt;
    }
}