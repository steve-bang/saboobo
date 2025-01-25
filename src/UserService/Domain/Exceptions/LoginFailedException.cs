
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Domain.Exceptions
{
    public class LoginFailedException : BadRequestException
    {
        public LoginFailedException() : base(
            "User_Login_Failed",
            "The phone number or password is incorrect.",
            "Please check your phone number and password and try again."
        )
        {
        }
    }
}