using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Sales");

            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.OrderId).HasColumnName("orderid").ValueGeneratedOnAdd();
            builder.Property(o => o.OrderDate).IsRequired().HasColumnName("orderdate");
            builder.Property(o => o.RequiredDate).IsRequired().HasColumnName("requireddate");
            builder.Property(o => o.ShippedDate).HasColumnName("shippeddate");
            builder.Property(o => o.Freight).IsRequired().HasColumnName("freight").HasDefaultValue(0).HasColumnType("money");
            builder.Property(o => o.ShipName).IsRequired().HasColumnName("shipname").HasMaxLength(40);
            builder.Property(o => o.ShipAddress).IsRequired().HasColumnName("shipaddress").HasMaxLength(60);
            builder.Property(o => o.ShipCity).IsRequired().HasColumnName("shipcity").HasMaxLength(15);
            builder.Property(o => o.ShipRegion).HasColumnName("shipregion").HasMaxLength(15);
            builder.Property(o => o.ShipPostalCode).HasColumnName("shippostalcode").HasMaxLength(10);
            builder.Property(o => o.ShipCountry).IsRequired().HasColumnName("shipcountry").HasMaxLength(15);

            builder.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Employee).WithMany(e => e.Orders).HasForeignKey(o => o.EmpId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Shipper).WithMany(s => s.Orders).HasForeignKey(o => o.ShipperId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(o => o.CustId).HasDatabaseName("idx_nc_custid");
            builder.HasIndex(o => o.EmpId).HasDatabaseName("idx_nc_empid");
            builder.HasIndex(o => o.ShipperId).HasDatabaseName("idx_nc_shipperid");
            builder.HasIndex(o => o.OrderDate).HasDatabaseName("idx_nc_orderdate");
            builder.HasIndex(o => o.ShippedDate).HasDatabaseName("idx_nc_shippeddate");
            builder.HasIndex(o => o.ShipPostalCode).HasDatabaseName("idx_nc_shippostalcode");
        }
    }
}