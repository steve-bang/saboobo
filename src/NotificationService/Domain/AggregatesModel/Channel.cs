
using SaBooBo.Domain.Shared;

namespace SaBooBo.NotificationService.Domain.AggregatesModel;

public class Channel : AggregateRoot
{
    public ChannelType ChannelType { get; private set; }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public Channel(ChannelType channelType, string name, string description)
    {
        ChannelType = channelType;
        Name = name;
        Description = description;
    }

    public static Channel Create(ChannelType channelType, string name, string description)
    {
        return new Channel(channelType, name, description);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow.ToUniversalTime();
    }

}