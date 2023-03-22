using FluentMigrator;

namespace SalesAndInventory.DatabaseUpgradeTool.Migrations
{
    [Migration(1)]
    public class CreateEmployeesTable : Migration
    {
        public override void Up()
        {
            Create.Schema("HR");

            Create.Table("Employees")
                .InSchema("HR")
                .WithColumn("empid").AsInt32().PrimaryKey().Identity()
                .WithColumn("lastname").AsString(20).NotNullable()
                .WithColumn("firstname").AsString(10).NotNullable()
                .WithColumn("title").AsString(30).NotNullable()
                .WithColumn("titleofcourtesy").AsString(25).NotNullable()
                .WithColumn("birthdate").AsDate().NotNullable()
                .WithColumn("hiredate").AsDate().NotNullable()
                .WithColumn("address").AsString(60).NotNullable()
                .WithColumn("city").AsString(15).NotNullable()
                .WithColumn("region").AsString(15).Nullable()
                .WithColumn("postalcode").AsString(10).Nullable()
                .WithColumn("country").AsString(15).NotNullable()
                .WithColumn("phone").AsString(24).NotNullable()
                .WithColumn("mgrid").AsInt32().Nullable();

            Create.ForeignKey("FK_Employees_Employees")
                .FromTable("Employees").InSchema("HR").ForeignColumn("mgrid")
                .ToTable("Employees").InSchema("HR").PrimaryColumn("empid");

            Execute.Sql("ALTER TABLE HR.Employees ADD CONSTRAINT CHK_birthdate CHECK (birthdate <= CAST(SYSDATETIME() AS DATE));");

            Create.Index("idx_nc_lastname")
                .OnTable("Employees").InSchema("HR")
                .OnColumn("lastname").Ascending()
                .WithOptions().NonClustered();

            Create.Index("idx_nc_postalcode")
                .OnTable("Employees").InSchema("HR")
                .OnColumn("postalcode").Ascending()
                .WithOptions().NonClustered();
        }

        public override void Down()
        {
            Delete.Index("idx_nc_postalcode").OnTable("Employees").InSchema("HR");
            Delete.Index("idx_nc_lastname").OnTable("Employees").InSchema("HR");
            Execute.Sql("ALTER TABLE HR.Employees DROP CONSTRAINT CHK_birthdate;");
            Delete.ForeignKey("FK_Employees_Employees").OnTable("Employees").InSchema("HR");
            Delete.Table("Employees").InSchema("HR");
            Delete.Schema("HR");
        }
    }
}