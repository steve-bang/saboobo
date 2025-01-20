
using System.Net;

namespace SaBooBo.Domain.Shared.ApiResponse;

public class ApiResponseError : BaseApiResponse
{
    public ErrorResponse Error { get; set; } = null!;

    public ApiResponseError(int httpStatus, ErrorResponse error)
    {
        Success = false;
        HttpStatus = httpStatus;
        Error = error;
    }

    public ApiResponseError(HttpStatusCode httpStatus, ErrorResponse error) : this((int)httpStatus, error)
    {
    }

    public ApiResponseError(ErrorResponse error) : this(HttpStatusCode.InternalServerError, error)
    {
    }
}

public class ErrorResponse
{
    public string Code { get; private set; } = null!;

    public string Message { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public ErrorResponse(string code, string message, string description)
    {
        Code = code;
        Message = message;
        Description = description;
    }
}