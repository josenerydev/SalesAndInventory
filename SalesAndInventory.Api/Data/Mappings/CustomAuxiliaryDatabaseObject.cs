using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Engine;
using NHibernate.Mapping;

namespace SalesAndInventory.Api.Data.Mappings
{
    public class CustomAuxiliaryDatabaseObject : IAuxiliaryDatabaseObject
    {
        public void AddDialectScope(string dialectName)
        { }

        public bool AppliesToDialect(Dialect dialect) => true;

        public string SqlCreateString(Dialect dialect, IMapping p, string defaultCatalog, string defaultSchema)
        {
            // Inclua aqui o DDL nativo para criar os índices e restrições
            return @"
            CREATE NONCLUSTERED INDEX idx_nc_lastname ON HR.Employees(lastname);
            CREATE NONCLUSTERED INDEX idx_nc_postalcode ON HR.Employees(postalcode);
            ALTER TABLE HR.Employees ADD CONSTRAINT CHK_birthdate CHECK(birthdate <= CAST(SYSDATETIME() AS DATE));
        ";
        }

        public string SqlDropString(Dialect dialect, string defaultCatalog, string defaultSchema)
        {
            // Inclua aqui o DDL nativo para excluir os índices e restrições
            return @"
            DROP INDEX idx_nc_lastname ON HR.Employees;
            DROP INDEX idx_nc_postalcode ON HR.Employees;
            ALTER TABLE HR.Employees DROP CONSTRAINT CHK_birthdate;
        ";
        }
    }
}