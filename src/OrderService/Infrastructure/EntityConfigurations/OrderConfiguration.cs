
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaBooBo.OrderService.Domain.AggregatesModel;

namespace SaBooBo.OrderService.Infrastructure.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigureOrderTable(builder);

            ConfigureOrderItemTable(builder);

            ConfigureShippingAddress(builder);

        }

        private static void ConfigureOrderTable(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.CustomerId)
                .IsRequired();
            
            builder.Property(o => o.ZaloOrderId)
                .IsRequired();

            builder.Property(o => o.Code)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.Subtotal)
                .IsRequired();

            builder.Property(o => o.TaxTotal)
                .IsRequired();

            builder.Property(o => o.ShippingTotal)
                .IsRequired();

            builder.Property(o => o.CostTotal)
                .IsRequired();

            builder.Property(o => o.IpAddress)
                .IsRequired(false);

            builder.Property(o => o.PaymentMethod)
                .IsRequired();

            builder.Property(o => o.OrderStatusLastChangedOn)
                .IsRequired(false);

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();

        }

        private static void ConfigureShippingAddress(EntityTypeBuilder<Order> builder)
        {
            // Configure OrderAddress as owned entity
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.ToTable("Shipping_Address");

                sa.HasKey("Id", "OrderId");

                sa.WithOwner().HasForeignKey("OrderId");

                sa.Property(sa => sa.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                sa.Property(sa => sa.PhoneNumber)
                    .IsRequired();

                sa.Property(sa => sa.Name)
                    .IsRequired();

                sa.Property(sa => sa.Email)
                    .IsRequired(false);

                sa.Property(sa => sa.Address)
                    .IsRequired();

                sa.Property(sa => sa.City)
                    .IsRequired();

                sa.Property(sa => sa.State)
                    .IsRequired();

                sa.Property(sa => sa.ZipCode)
                    .IsRequired();

                sa.Property(sa => sa.Country)
                    .IsRequired();

                sa.Property(sa => sa.UpdatedAt)
                    .IsRequired();

                sa.Property(sa => sa.CreatedAt)
                    .IsRequired();

                // Configure the navigation with field access, this is required because the navigation is a value object
                builder.Metadata.FindNavigation(nameof(Order.ShippingAddress))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });
        }

        private static void ConfigureOrderItemTable(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsMany(o => o.Items, sb =>
            {
                sb.Property(oi => oi.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                sb.ToTable("OrderItem");

                sb.HasKey("Id", "OrderId");

                sb.Ignore(oi => oi.TotalPrice);

                sb.WithOwner().HasForeignKey("OrderId");

                sb.Property(oi => oi.ProductName)
                    .IsRequired();

                sb.Property(oi => oi.ImageUrl)
                    .IsRequired();

                sb.Property(oi => oi.Quantity)
                    .IsRequired();

                sb.Property(oi => oi.UnitPrice)
                    .IsRequired();

                sb.Property(oi => oi.Notes)
                    .IsRequired(false);

                sb.Property(oi => oi.UpdatedAt)
                    .IsRequired();

                sb.Property(oi => oi.CreatedAt)
                    .IsRequired();

            });

            // Configure the navigation with field access, this is required because the navigation is a collection of value objects
            builder.Metadata.FindNavigation(nameof(Order.Items))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }

    }



}