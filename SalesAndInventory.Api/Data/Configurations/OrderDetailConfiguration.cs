using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails", "Sales");

            builder.HasKey(e => new { e.OrderId, e.ProductId });

            builder.Property(e => e.OrderId)
                .HasColumnName("orderid");

            builder.Property(e => e.ProductId)
                .HasColumnName("productid");

            builder.Property(e => e.UnitPrice)
                .HasColumnName("unitprice")
                .HasColumnType("money")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(e => e.Qty)
                .HasColumnName("qty")
                .HasColumnType("smallint")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.Discount)
                .HasColumnName("discount")
                .HasColumnType("numeric(4,3)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(e => e.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderDetails_Orders");

            builder.HasOne(e => e.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderDetails_Products");

            builder.HasCheckConstraint("CHK_discount", "[discount] BETWEEN 0 AND 1");
            builder.HasCheckConstraint("CHK_qty", "[qty] > 0");
            builder.HasCheckConstraint("CHK_unitprice", "[unitprice] >= 0");

            builder.HasIndex(e => e.OrderId)
                .HasDatabaseName("idx_nc_orderid");

            builder.HasIndex(e => e.ProductId)
                .HasDatabaseName("idx_nc_productid");
        }
    }
}