
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SaBooBo.Product.Infrastructure;

public class ProductConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.Product>
{
    public void Configure(EntityTypeBuilder<Domain.AggregatesModel.Product> builder)
    {
        ConfigureProductTable(builder);

        ConfigureToppingTable(builder);
    }

    private static void ConfigureProductTable(EntityTypeBuilder<Domain.AggregatesModel.Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id) // Configure the property
        .ValueGeneratedNever(); // Never generate a value for this property

        builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(500);

        builder.Property(x => x.Description)
        .IsRequired()
        .HasMaxLength(1000);

        builder.Property(x => x.CreatedDate)
        .HasDefaultValue(DateTime.UtcNow);

    }

    private static void ConfigureToppingTable(EntityTypeBuilder<Domain.AggregatesModel.Product> builder)
    {
        builder.OwnsMany(menu => menu.Toppings,
        sb =>
        {

            sb.ToTable("Topping");

            sb.HasKey("Id", "ProductId");

            sb.WithOwner().HasForeignKey("ProductId");

            sb.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

            sb.Property(x => x.Price)
            .IsRequired();

            sb.Property(x => x.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow);
        });

        // Configure the navigation with field access, this is required because the navigation is a collection of value objects
        builder.Metadata.FindNavigation(nameof(Domain.AggregatesModel.Product.Toppings))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}