
namespace SaBooBo.NotificationService.Models.DTO;

public class ChannelConfigCommandRequestDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

    public ChannelConfigCommandRequestDto(string name, string description, IDictionary<string, string> metadata)
    {
        Name = name;
        Description = description;
        Metadata = metadata;
    }
}