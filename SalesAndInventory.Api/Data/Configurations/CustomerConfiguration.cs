using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "Sales");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CompanyName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.ContactName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.ContactTitle)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Address)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.City)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.Region)
                .HasMaxLength(15);

            builder.Property(c => c.PostalCode)
                .HasMaxLength(10);

            builder.Property(c => c.Country)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasMaxLength(24)
                .IsRequired();

            builder.Property(c => c.Fax)
                .HasMaxLength(24);

            builder.HasIndex(c => c.City)
                .HasDatabaseName("idx_nc_city");

            builder.HasIndex(c => c.CompanyName)
                .HasDatabaseName("idx_nc_companyname");

            builder.HasIndex(c => c.PostalCode)
                .HasDatabaseName("idx_nc_postalcode");

            builder.HasIndex(c => c.Region)
                .HasDatabaseName("idx_nc_region");
        }
    }
}