
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Domain.Exceptions
{
    public class UserAddressNotFoundException : NotFoundException
    {
        public UserAddressNotFoundException() : base(
            "Address_Not_Found",
            "The address was not found in the system.",
            string.Empty
        )
        {
        }
    }
}