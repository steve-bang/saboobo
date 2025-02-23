
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.MerchantService.Domain.Exceptions;

public class InvalidDeserializeObjectMetadataException : BadRequestException
{
    public InvalidDeserializeObjectMetadataException(string objectName, Type type) : base(
        "Invalid_Deserialize_Object_Metadata",
        $"Invalid {objectName} object metadata.",
        $"Please check the {objectName} object metadata and try again. The metadata must be a valid {type.Name} object."
    )
    {
    }
}