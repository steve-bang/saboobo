
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.UserService.Domain.Exceptions
{
    /// <summary>
    /// The user register with Zalo is incorrect.
    /// When the user register with Zalo external provider and the user is incorrect data.
    /// </summary>
    public class ExternalUserZaloIncorrectException : BadRequestException
    {
        public ExternalUserZaloIncorrectException() : base(
            "User_External_Zalo_Incorrect",
            "The user register with Zalo is incorrect.",
            string.Empty
        )
        {
        }
    }
}