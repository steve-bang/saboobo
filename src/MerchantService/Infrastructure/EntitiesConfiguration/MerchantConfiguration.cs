
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.MerchantService.Domain.AggregatesModel;

namespace MerchantService.Infrastructure.EntitiesConfiguration
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("Merchant");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.EmailAddress)
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(1000);

            builder.Property(x => x.CoverUrl)
                .HasMaxLength(1000);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.OAUrl)
                .HasMaxLength(1000);

            builder.Property(x => x.Website)
                .HasMaxLength(1000);

            builder.Property(x => x.Address)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}