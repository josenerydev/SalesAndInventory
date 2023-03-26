using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "Sales");

            builder.HasKey(c => c.CustId);
            builder.Property(c => c.CustId).HasColumnName("custid").ValueGeneratedOnAdd();
            builder.Property(c => c.CompanyName).IsRequired().HasColumnName("companyname").HasMaxLength(40);
            builder.Property(c => c.ContactName).IsRequired().HasColumnName("contactname").HasMaxLength(30);
            builder.Property(c => c.ContactTitle).IsRequired().HasColumnName("contacttitle").HasMaxLength(30);
            builder.Property(c => c.Address).IsRequired().HasColumnName("address").HasMaxLength(60);
            builder.Property(c => c.City).IsRequired().HasColumnName("city").HasMaxLength(15);
            builder.Property(c => c.Region).HasColumnName("region").HasMaxLength(15);
            builder.Property(c => c.PostalCode).HasColumnName("postalcode").HasMaxLength(10);
            builder.Property(c => c.Country).IsRequired().HasColumnName("country").HasMaxLength(15);
            builder.Property(c => c.Phone).IsRequired().HasColumnName("phone").HasMaxLength(24);
            builder.Property(c => c.Fax).HasColumnName("fax").HasMaxLength(24);

            builder.HasIndex(c => c.City).HasDatabaseName("idx_nc_city");
            builder.HasIndex(c => c.CompanyName).HasDatabaseName("idx_nc_companyname");
            builder.HasIndex(c => c.PostalCode).HasDatabaseName("idx_nc_postalcode");
            builder.HasIndex(c => c.Region).HasDatabaseName("idx_nc_region");
        }
    }
}