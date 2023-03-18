using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data.Mappings
{
    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Schema("HR");
            Table("Employees");

            Id(x => x.EmpId, m =>
            {
                m.Column("empid");
                m.Generator(Generators.Identity);
            });

            Property(x => x.LastName, m => m.Column("lastname"));
            Property(x => x.FirstName, m => m.Column("firstname"));
            Property(x => x.Title, m => m.Column("title"));
            Property(x => x.TitleOfCourtesy, m => m.Column("titleofcourtesy"));
            Property(x => x.BirthDate, m => m.Column("birthdate"));
            Property(x => x.HireDate, m => m.Column("hiredate"));
            Property(x => x.Address, m => m.Column("address"));
            Property(x => x.City, m => m.Column("city"));
            Property(x => x.Region, m => m.Column("region"));
            Property(x => x.PostalCode, m => m.Column("postalcode"));
            Property(x => x.Country, m => m.Column("country"));
            Property(x => x.Phone, m => m.Column("phone"));

            ManyToOne(x => x.Manager, m =>
            {
                m.Column("mgrid");
                m.ForeignKey("FK_Employees_Employees");
                m.Cascade(Cascade.None);
            });
        }
    }
}