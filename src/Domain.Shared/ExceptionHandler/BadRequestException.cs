
using System.Net;

namespace SaBooBo.Domain.Shared.ExceptionHandler;

public class BadRequestException : SaBooBoException
{
    public BadRequestException(
        string code,
        string message,
        string description) : base(HttpStatusCode.BadRequest, code, message, description)
    {
    }
}