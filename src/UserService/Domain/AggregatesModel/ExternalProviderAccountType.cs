
namespace SaBooBo.UserService.Domain.AggregatesModel;

public enum ExternalProviderAccount
{
    Zalo,
    Facebook,
    Google
}

public class UserInfoZaloProvider
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public bool FollowedOA { get; set; }
    public string PhoneNumber { get; set; } = null!;

    public UserInfoZaloProvider(string id, string name, string avatar, bool followedOA, string phoneNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Avatar = avatar;
        this.FollowedOA = followedOA;
        this.PhoneNumber = phoneNumber;
    }

    public static UserInfoZaloProvider Create(string id, string name, string avatar, bool followedOA, string phoneNumber)
    {
        return new UserInfoZaloProvider(id, name, avatar, followedOA, phoneNumber);
    }
}