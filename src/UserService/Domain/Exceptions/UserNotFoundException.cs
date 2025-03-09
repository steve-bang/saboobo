
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Domain.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base(
            "User_Not_Found",
            "The user was not found in the system.",
            string.Empty
        )
        {
        }
    }
}