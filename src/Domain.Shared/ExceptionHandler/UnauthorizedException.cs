
namespace SaBooBo.Domain.Shared.ExceptionHandler;

public class UnauthorizedException : SaBooBoException
{
    public UnauthorizedException(
        string code,
        string message,
        string description) : base(System.Net.HttpStatusCode.Unauthorized, code, message, description)
    {
    }

    public UnauthorizedException() : this(
        "Unauthorized",
        "Unauthorized",
        "You are not authorized to access this resource."
    )
    {
    }
}