
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.UserService.Domain.AggregatesModel;

namespace SaBooBo.UserService.Infrastructure.EntitiesConfiguration;


/// <summary>
/// User configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SaBooBo.UserService.Domain.AggregatesModel.User}" />
/// dotnet ef migrations add RemoveAddressIsNotNull --context UserAppContext
public class UserExternalProviderConfiguration : IEntityTypeConfiguration<UserExtenalProvider>
{
    public void Configure(EntityTypeBuilder<UserExtenalProvider> builder)
    {
        builder.ToTable("UserExternalProvider");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UserExternalId)
            .IsRequired();

        builder.Property(e => e.Provider)
            .HasConversion<string>() // This is required to store the enum as string in the database
            .IsRequired();

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
