using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Production");

            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryId).HasColumnName("categoryid").ValueGeneratedOnAdd();
            builder.Property(c => c.CategoryName).IsRequired().HasColumnName("categoryname").HasMaxLength(15);
            builder.Property(c => c.Description).IsRequired().HasColumnName("description").HasMaxLength(200);

            builder.HasIndex(c => c.CategoryName).HasDatabaseName("idx_nc_categoryname");
        }
    }
}