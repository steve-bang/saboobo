
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.NotificationService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Infrastructure.EntityConfigurations
{
    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {

        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("Channel");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Name)
                .IsRequired();

            builder.Property(o => o.Description)
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();
        }

    }

}