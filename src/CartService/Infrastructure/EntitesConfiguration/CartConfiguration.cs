
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaBooBo.CartService.Infrastructure.EntitesConfiguration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            ConfigureCartTable(builder);

            ConfigureCartItemTable(builder);
        }

        public void ConfigureCartTable(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.CustomerId)
                .IsRequired();

            builder.Property(c => c.TotalPrice)
                .IsRequired();


            builder.Property(c => c.UpdatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

        }

        private static void ConfigureCartItemTable(EntityTypeBuilder<Cart> builder)
        {
            builder.OwnsMany(menu => menu.Items,
            sb =>
            {
                sb.Property(c => c.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                sb.ToTable("CartItem");

                sb.HasKey("Id", "CartId");

                sb.Ignore(x => x.TotalPrice);

                sb.WithOwner().HasForeignKey("CartId");

                sb.Property(x => x.ProductName)
                    .IsRequired();
                
                sb.Property(x => x.ProductImage)
                    .IsRequired(false);

                sb.Property(x => x.Quantity)
                    .IsRequired();

                sb.Property(x => x.UnitPrice)
                    .IsRequired();

                sb.Property(x => x.Notes)
                    .IsRequired(false);

                sb.Property(x => x.UpdatedAt)
                    .IsRequired();

                sb.Property(x => x.CreatedAt);

            });

            // Configure the navigation with field access, this is required because the navigation is a collection of value objects
            builder.Metadata.FindNavigation(nameof(Cart.Items))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}