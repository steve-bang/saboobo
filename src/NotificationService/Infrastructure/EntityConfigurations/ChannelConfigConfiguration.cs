
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Infrastructure.EntityConfigurations
{
    public class ChannelConfigConfiguration : IEntityTypeConfiguration<ChannelConfig>
    {

        public void Configure(EntityTypeBuilder<ChannelConfig> builder)
        {
            builder.ToTable("ChannelConfig");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Name)
                .IsRequired();

            builder.Property(o => o.Description)
                .IsRequired();

            // Convert the metadata dictionary to a JSON string before storing it in the database.
            builder.Property(o => o.Metadata)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                )
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();
        }

    }

}