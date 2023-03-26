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

            builder.HasKey(od => new { od.OrderId, od.ProductId });
            builder.Property(od => od.OrderId).HasColumnName("orderid");
            builder.Property(od => od.ProductId).HasColumnName("productid");
            builder.Property(od => od.UnitPrice).IsRequired().HasColumnName("unitprice").HasDefaultValue(0).HasColumnType("money");
            builder.Property(od => od.Qty).IsRequired().HasColumnName("qty").HasDefaultValue(1).HasColumnType("smallint");
            builder.Property(od => od.Discount).IsRequired().HasColumnName("discount").HasDefaultValue(0).HasColumnType("numeric(4, 3)");

            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(od => od.Product).WithMany(p => p.OrderDetails).HasForeignKey(od => od.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(od => od.OrderId).HasDatabaseName("idx_nc_orderid");
            builder.HasIndex(od => od.ProductId).HasDatabaseName("idx_nc_productid");
        }
    }
}