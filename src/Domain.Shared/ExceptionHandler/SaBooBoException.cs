
using System.Net;
using SaBooBo.Domain.Shared.ApiResponse;

namespace SaBooBo.Domain.Shared.ExceptionHandler;

public class SaBooBoException : Exception
{
    public ApiResponseError Error { get; set; } = null!;

    public SaBooBoException(string code, string message, string description) : base(message)
    {
        Error = new ApiResponseError(
            new ErrorResponse(code, message, description)
        );
    }

    public SaBooBoException(int httpStatus, string code, string message, string description) : base(message)
    {
        Error = new ApiResponseError(
            httpStatus,
            new ErrorResponse(code, message, description)
        );
    }

    public SaBooBoException(HttpStatusCode httpStatus, string code, string message, string description)
        : this((int)httpStatus, code, message, description)
    {

    }

    public SaBooBoException(Exception exception)
        : this(HttpStatusCode.InternalServerError, "ServerError", exception.Message, exception.HelpLink ?? string.Empty)
    {

    }
}