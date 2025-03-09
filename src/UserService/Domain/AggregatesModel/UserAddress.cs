
namespace SaBooBo.UserService.Domain.AggregatesModel
{
    public class UserAddress : AggregateRoot
    {
        /// <summary>
        /// The user id that this address belongs to
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// The contact name for this address.
        /// </summary>
        /// <example>John Doe</example>
        public string ContactName { get; private set; }

        /// <summary>
        /// The phone number for this address.
        /// </summary>
        /// <example>+84987654321</example>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        /// <example>123 Main St</example>
        public string AddressLine1 { get; private set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        /// <example>Apartment 123</example>
        public string AddressLine2 { get; private set; }

        /// <summary>
        /// The city of the address.
        /// </summary>
        /// <example>Quan 1</example>
        public string City { get; private set; }

        /// <summary>
        /// The state of the address.
        /// </summary>
        /// <example>Ho Chi Minh</example>
        public string State { get; private set; }

        /// <summary>
        /// The country of the address.
        /// </summary>
        /// <example>Vietnam</example>
        public string Country { get; private set; }

        /// <summary>
        /// Whether this address is the default address for the user.
        /// </summary>
        /// <example>true</example>
        public bool IsDefault { get; private set; } = false;

        /// <summary>
        /// The full address of the address.
        /// </summary>
        /// <example>123 Main St, Apartment 123, Quan 1, Ho Chi Minh, Vietnam</example>
        public string FullAddress => $"{AddressLine1}, {AddressLine2}, {City}, {State}, {Country}";

        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();


        /// <summary>
        /// Constructor for creating a new address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="addressLine1"></param>
        /// <param name="addressLine2"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="isDefault"></param>
        public UserAddress(Guid userId, string contactName, string phoneNumber, string addressLine1, string addressLine2, string city, string state, string country, bool isDefault = false)
        {
            Id = CreateNewId();
            UserId = userId;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            Country = country;
            IsDefault = isDefault;
        }

        public static UserAddress Create(Guid userId, string contactName, string phoneNumber, string addressLine1, string addressLine2, string city, string state, string country, bool isDefault = false)
        {
            return new UserAddress(userId, contactName, phoneNumber, addressLine1, addressLine2, city, state, country, isDefault);
        }


        public void Update(string contactName, string phoneNumber, string addressLine1, string addressLine2, string city, string state, string country, bool isDefault)
        {
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            Country = country;
            IsDefault = isDefault;
            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }

        public void SetAsDefault()
        {
            IsDefault = true;
            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }

        public void SetAsNotDefault()
        {
            IsDefault = false;
            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }
    }
}