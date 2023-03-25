using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Sales");

            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(o => o.RequiredDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(o => o.ShippedDate)
                .HasColumnType("date");

            builder.Property(o => o.Freight)
                .HasColumnType("money")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(o => o.ShipName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(o => o.ShipAddress)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(o => o.ShipCity)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(o => o.ShipRegion)
                .HasMaxLength(15);

            builder.Property(o => o.ShipPostalCode)
                .HasMaxLength(10);

            builder.Property(o => o.ShipCountry)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Shipper)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShipperId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(o => o.CustomerId)
                .HasDatabaseName("idx_nc_custid");

            builder.HasIndex(o => o.EmployeeId)
                .HasDatabaseName("idx_nc_empid");

            builder.HasIndex(o => o.ShipperId)
                .HasDatabaseName("idx_nc_shipperid");

            builder.HasIndex(o => o.OrderDate)
                .HasDatabaseName("idx_nc_orderdate");

            builder.HasIndex(o => o.ShippedDate)
                .HasDatabaseName("idx_nc_shippeddate");

            builder.HasIndex(o => o.ShipPostalCode)
                .HasDatabaseName("idx_nc_shippostalcode");
        }
    }
}