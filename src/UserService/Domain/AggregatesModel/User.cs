

namespace SaBooBo.UserService.Domain.AggregatesModel
{
    public class User : AggregateRoot
    {
        public Guid? MerchantId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? AvatarUrl { get; private set; }
        public string PhoneNumber { get; private set; } = null!;
        public string? EmailAddress { get; private set; }
        public string PasswordHash { get; private set; } = null!;
        public string PasswordSalt { get; private set; } = null!;
        public bool IsActive { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User(Guid? merchantId, string name, string phoneNumber, string passwordHash, string passwordSalt, string? avatarUrl)
        {
            Id = CreateNewId();

            MerchantId = merchantId;
            Name = name;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            IsActive = true;
            AvatarUrl = avatarUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(Guid? merchantId, string name, string phoneNumber, string passwordHash, string passwordSalt, string avatarUrl)
        {
            return new User(merchantId, name, phoneNumber, passwordHash, passwordSalt, avatarUrl);
        }

        public static User Create(Guid? merchantId, string phoneNumber, string passwordHash, string passwordSalt)
        {
            return new User(merchantId, string.Empty, phoneNumber, passwordHash, passwordSalt, null);
        }

        public void Update(string name, string phoneNumber, string? emailAddress, string? avatarUrl)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            AvatarUrl = avatarUrl;
        }

        public void UpdatePassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void UpdateLastLoginAt()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void UpdateAvatarUrl(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
        }
    }
}