using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "Production");

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).HasColumnName("productid").ValueGeneratedOnAdd();
            builder.Property(p => p.ProductName).IsRequired().HasColumnName("productname").HasMaxLength(40);
            builder.Property(p => p.UnitPrice).IsRequired().HasColumnName("unitprice").HasDefaultValue(0).HasColumnType("money");
            builder.Property(p => p.Discontinued).IsRequired().HasColumnName("discontinued").HasDefaultValue(false);

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CategoryId).HasDatabaseName("idx_nc_categoryid");
            builder.HasIndex(p => p.ProductName).HasDatabaseName("idx_nc_productname");
            builder.HasIndex(p => p.SupplierId).HasDatabaseName("idx_nc_supplierid");

            builder.HasCheckConstraint("CHK_Products_unitprice", "unitprice >= 0");
        }
    }
}