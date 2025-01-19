
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.Product.Domain.AggregatesModel;

namespace SaBooBo.Product.Infrastructure;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoryTable(builder);

    }

    private void ConfigureCategoryTable(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id) // Configure the property
        .HasColumnOrder(0)
        .ValueGeneratedNever(); // Never generate a value for this property

        builder.Property(x => x.MerchantId);

        builder.Property(x => x.Code)
        .HasMaxLength(100);

        builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(500);

        builder.Property(x => x.Description)
        .HasMaxLength(1000);

        builder.Property(x => x.TotalProduct);

        builder.Property(x => x.CreatedDate);
    }
}