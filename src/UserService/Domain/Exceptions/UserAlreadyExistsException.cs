
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Domain.Exceptions
{
    public class UserAlreadyExistsException : BadRequestException
    {
        public UserAlreadyExistsException() : base(
            "User_Already_Exists",
            "The user already exists in the system.",
            string.Empty
        )
        {
        }
    }
}