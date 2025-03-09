
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Infrastructure.EntitiesConfiguration;


/// <summary>
/// User configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SaBooBo.UserService.Domain.AggregatesModel.User}" />
/// dotnet ef migrations add RemoveAddressIsNotNull --context UserAppContext
public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("UserAddress");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();



        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.ContactName)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .IsRequired();

        builder.Property(e => e.AddressLine1)
            .IsRequired();
        
        builder.Property(e => e.AddressLine2)
            .IsRequired(false);

        builder.Property(e => e.City)
            .IsRequired();

        builder.Property(e => e.State)
            .IsRequired();

        builder.Property(e => e.Country)
            .IsRequired();

        builder.Property(e => e.IsDefault)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
