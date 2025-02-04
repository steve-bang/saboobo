
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MerchantService.Infrastructure.EntitiesConfiguration;

public class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banner");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.MerchantId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(1000);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}