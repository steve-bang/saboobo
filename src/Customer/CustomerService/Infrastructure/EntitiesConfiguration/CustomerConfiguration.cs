
namespace SaBooBo.CustomerService.Infrastructure.EntitiesConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SaBooBo.CustomerService.Domain.AggregatesModel;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.MerchantId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.EmailAddress)
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.AvatarUrl)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.DateOfBirth);

            builder.Property(x => x.Gender)
                .HasConversion(
                    v => (short)v, // Parse the enum to string.
                    value => (Gender)value // Parse the string to enum.
                );

            builder.Property(x => x.CreatedDate)
                .IsRequired();

        }
    }
}