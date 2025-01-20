
using System.Net;

namespace SaBooBo.Domain.Shared.ExceptionHandler;

public class NotFoundException : SaBooBoException
{
    public NotFoundException(
        string code,
        string message,
        string description) : base(HttpStatusCode.NotFound, code, message, description)
    {
    }
}