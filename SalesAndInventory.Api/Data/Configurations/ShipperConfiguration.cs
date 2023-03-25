using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shippers", "Sales");

            builder.HasKey(s => s.ShipperId);

            builder.Property(s => s.CompanyName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(s => s.Phone)
                .HasMaxLength(24)
                .IsRequired();
        }
    }
}