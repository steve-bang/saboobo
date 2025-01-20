
namespace SaBooBo.Domain.Shared.ApiResponse;

public class BaseApiResponse
{
    public bool Success { get; protected set; }

    public int HttpStatus { get; protected set; }
}