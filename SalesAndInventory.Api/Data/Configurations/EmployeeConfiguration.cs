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

            builder.HasKey(e => e.EmpId)
                .HasName("PK_Employees");

            builder.Property(e => e.EmpId)
                .HasColumnName("empid")
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(e => e.LastName)
                .HasColumnName("lastname")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.FirstName)
                .HasColumnName("firstname")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.TitleOfCourtesy)
                .HasColumnName("titleofcourtesy")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.BirthDate)
                .HasColumnName("birthdate")
                .IsRequired();

            builder.Property(e => e.HireDate)
                .HasColumnName("hiredate")
                .IsRequired();

            builder.Property(e => e.Address)
                .HasColumnName("address")
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.City)
                .HasColumnName("city")
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Region)
                .HasColumnName("region")
                .HasMaxLength(15);

            builder.Property(e => e.PostalCode)
                .HasColumnName("postalcode")
                .HasMaxLength(10);

            builder.Property(e => e.Country)
                .HasColumnName("country")
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Phone)
                .HasColumnName("phone")
                .IsRequired()
                .HasMaxLength(24);

            builder.Property(e => e.ManagerId)
                .HasColumnName("mgrid");

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .HasConstraintName("FK_Employees_Employees")
                .IsRequired(false);

            builder.HasCheckConstraint("CHK_birthdate", "birthdate <= CAST(SYSDATETIME() AS DATE)");

            builder.HasIndex(e => e.LastName)
                .HasDatabaseName("idx_nc_lastname")
                .IsUnique(false);

            builder.HasIndex(e => e.PostalCode)
                .HasDatabaseName("idx_nc_postalcode")
                .IsUnique(false);
        }
    }
}