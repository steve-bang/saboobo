
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MerchantService.Infrastructure.EntitiesConfiguration
{
    public class MerchantProviderSettingConfiguration : IEntityTypeConfiguration<MerchantProviderSetting>
    {
        public void Configure(EntityTypeBuilder<MerchantProviderSetting> builder)
        {
            builder.ToTable("MerchantProviderSetting");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.MerchantId)
                .IsRequired();

            builder.Property(x => x.ProviderType)
                .HasConversion<string>()
                .IsRequired();
            
            builder.Property(x => x.Metadata)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}