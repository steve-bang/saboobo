
namespace SaBooBo.UserService.Domain.AggregatesModel;

public class UserExtenalProvider : AggregateRoot
{
    public string UserExternalId { get; private set; } = null!;
    public ExternalProviderAccount Provider { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public UserExtenalProvider(string userExternalId, ExternalProviderAccount provider, Guid userId)
    {
        Id = CreateNewId();

        UserExternalId = userExternalId;
        Provider = provider;
        UserId = userId;

        CreatedAt = DateTime.UtcNow.ToUniversalTime();
    }

    public static UserExtenalProvider Create(string userExternalId, ExternalProviderAccount provider, Guid userId)
    {
        return new UserExtenalProvider(userExternalId, provider, userId);
    }

    public void UpdateUserId(Guid userId)
    {
        UserId = userId;
    }

    public void UpdateUserExternalId(string userExternalId)
    {
        UserExternalId = userExternalId;
    }

    public void UpdateProvider(ExternalProviderAccount provider)
    {
        Provider = provider;
    }

}