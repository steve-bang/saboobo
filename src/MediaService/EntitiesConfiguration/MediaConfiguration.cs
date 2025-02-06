
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.MediaService.Models;

namespace SaBooBo.MediaService.EntitiesConfiguration
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Media");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.Property(x => x.ContentType).IsRequired();
            builder.Property(x => x.AccessTier).IsRequired();
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}