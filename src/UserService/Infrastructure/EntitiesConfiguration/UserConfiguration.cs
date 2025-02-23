
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Infrastructure.EntitiesConfiguration;


/// <summary>
/// User configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SaBooBo.UserService.Domain.AggregatesModel.User}" />
/// dotnet ef migrations add RemoveAddressIsNotNull --context UserAppContext
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.MerchantId)
            .IsRequired(false);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.EmailAddress)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasIndex(e => e.EmailAddress);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(e => e.PhoneNumber)
            .IsUnique();

        builder.Property(e => e.PasswordHash)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(e => e.PasswordSalt)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired();

        builder.Property(e => e.LastLoginAt);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
