using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "HR");

            builder.HasKey(e => e.EmpId);
            builder.Property(e => e.EmpId).HasColumnName("empid").ValueGeneratedOnAdd();
            builder.Property(e => e.LastName).IsRequired().HasColumnName("lastname").HasMaxLength(20);
            builder.Property(e => e.FirstName).IsRequired().HasColumnName("firstname").HasMaxLength(10);
            builder.Property(e => e.Title).IsRequired().HasColumnName("title").HasMaxLength(30);
            builder.Property(e => e.TitleOfCourtesy).IsRequired().HasColumnName("titleofcourtesy").HasMaxLength(25);
            builder.Property(e => e.BirthDate).IsRequired().HasColumnName("birthdate");
            builder.Property(e => e.HireDate).IsRequired().HasColumnName("hiredate");
            builder.Property(e => e.Address).IsRequired().HasColumnName("address").HasMaxLength(60);
            builder.Property(e => e.City).IsRequired().HasColumnName("city").HasMaxLength(15);
            builder.Property(e => e.Region).HasColumnName("region").HasMaxLength(15);
            builder.Property(e => e.PostalCode).HasColumnName("postalcode").HasMaxLength(10);
            builder.Property(e => e.Country).IsRequired().HasColumnName("country").HasMaxLength(15);
            builder.Property(e => e.Phone).IsRequired().HasColumnName("phone").HasMaxLength(24);
            builder.Property(e => e.ManagerId).HasColumnName("mgrid");

            builder.HasOne(e => e.Manager)
                   .WithMany(e => e.Subordinates)
                   .HasForeignKey(e => e.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasCheckConstraint("CHK_birthdate", "birthdate <= CAST(SYSDATETIME() AS DATE)");
        }
    }
}