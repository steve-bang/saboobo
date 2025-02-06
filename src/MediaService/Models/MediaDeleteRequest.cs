
namespace SaBooBo.MediaService.Models;

public class MediaDeleteRequest
{
    /// <summary>
    /// The URL of the file to delete.
    /// </summary>
    public string FileUrl { get; set; } = null!;
}