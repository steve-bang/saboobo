
namespace SaBooBo.MediaService.Models
{
    public class Media
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public long Size { get; set; }

        public string ContentType { get; set; } = null!;

        public string AccessTier { get; set; } = null!;

        public string Url { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();


        public Media()
        {
            Id = Guid.NewGuid();
        }

        public Media(Guid userId, string name, long size, string contentType, string accessTier, string url)
            : this()
        {
            UserId = userId;
            Name = name;
            Size = size;
            ContentType = contentType;
            AccessTier = accessTier;
            Url = url;
        }

    }
}