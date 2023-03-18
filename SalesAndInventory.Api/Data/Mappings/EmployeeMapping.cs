using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Mappings
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Schema("HR");
            Table("Employees");

            Id(e => e.Id, m => m.Generator(Generators.Identity));

            Property(e => e.LastName, m =>
            {
                m.Column("lastname");
                m.Length(20);
                m.NotNullable(true);
            });

            Property(e => e.FirstName, m =>
            {
                m.Column("firstname");
                m.Length(10);
                m.NotNullable(true);
            });

            Property(e => e.Title, m =>
            {
                m.Column("title");
                m.Length(30);
                m.NotNullable(true);
            });

            Property(e => e.TitleOfCourtesy, m =>
            {
                m.Column("titleofcourtesy");
                m.Length(25);
                m.NotNullable(true);
            });

            Property(e => e.BirthDate, m =>
            {
                m.Column("birthdate");
                m.NotNullable(true);
                m.Check($"birthdate <= CAST({DateTime.UtcNow:yyyy-MM-dd} AS DATE)");
            });

            Property(e => e.HireDate, m =>
            {
                m.Column("hiredate");
                m.NotNullable(true);
            });

            Property(e => e.Address, m =>
            {
                m.Column("address");
                m.Length(60);
                m.NotNullable(true);
            });

            Property(e => e.City, m =>
            {
                m.Column("city");
                m.Length(15);
                m.NotNullable(true);
            });

            Property(e => e.Region, m =>
            {
                m.Column("region");
                m.Length(15);
            });

            Property(e => e.PostalCode, m =>
            {
                m.Column("postalcode");
                m.Length(10);
            });

            Property(e => e.Country, m =>
            {
                m.Column("country");
                m.Length(15);
                m.NotNullable(true);
            });

            Property(e => e.Phone, m =>
            {
                m.Column("phone");
                m.Length(24);
                m.NotNullable(true);
            });

            ManyToOne(e => e.Manager, m =>
            {
                m.Column("mgrid");
                m.NotNullable(false);
            });

            Index("idx_nc_lastname", x => x.LastName);
            Index("idx_nc_postalcode", x => x.PostalCode);
        }
    }
}