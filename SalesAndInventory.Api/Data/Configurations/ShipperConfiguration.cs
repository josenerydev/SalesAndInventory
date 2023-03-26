using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shippers", "Sales");

            builder.HasKey(s => s.ShipperId);
            builder.Property(s => s.ShipperId).HasColumnName("shipperid").ValueGeneratedOnAdd();
            builder.Property(s => s.CompanyName).IsRequired().HasColumnName("companyname").HasMaxLength(40);
            builder.Property(s => s.Phone).IsRequired().HasColumnName("phone").HasMaxLength(24);
        }
    }
}