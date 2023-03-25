using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "Production");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(p => p.UnitPrice)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(p => p.Discontinued)
                .IsRequired();

            builder.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CategoryId)
                .HasDatabaseName("idx_nc_categoryid");

            builder.HasIndex(p => p.ProductName)
                .HasDatabaseName("idx_nc_productname");

            builder.HasIndex(p => p.SupplierId)
                .HasDatabaseName("idx_nc_supplierid");
        }
    }
}