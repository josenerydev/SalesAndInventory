using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers", "Production");

            builder.HasKey(s => s.SupplierId);
            builder.Property(s => s.SupplierId).HasColumnName("supplierid").ValueGeneratedOnAdd();
            builder.Property(s => s.CompanyName).IsRequired().HasColumnName("companyname").HasMaxLength(40);
            builder.Property(s => s.ContactName).IsRequired().HasColumnName("contactname").HasMaxLength(30);
            builder.Property(s => s.ContactTitle).IsRequired().HasColumnName("contacttitle").HasMaxLength(30);
            builder.Property(s => s.Address).IsRequired().HasColumnName("address").HasMaxLength(60);
            builder.Property(s => s.City).IsRequired().HasColumnName("city").HasMaxLength(15);
            builder.Property(s => s.Region).HasColumnName("region").HasMaxLength(15);
            builder.Property(s => s.PostalCode).HasColumnName("postalcode").HasMaxLength(10);
            builder.Property(s => s.Country).IsRequired().HasColumnName("country").HasMaxLength(15);
            builder.Property(s => s.Phone).IsRequired().HasColumnName("phone").HasMaxLength(24);
            builder.Property(s => s.Fax).HasColumnName("fax").HasMaxLength(24);

            builder.HasIndex(s => s.CompanyName).HasDatabaseName("idx_nc_companyname");
            builder.HasIndex(s => s.PostalCode).HasDatabaseName("idx_nc_postalcode");
        }
    }
}