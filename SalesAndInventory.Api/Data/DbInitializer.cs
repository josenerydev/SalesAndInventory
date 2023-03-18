using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Data
{
    public class DbInitializer
    {
        public static void InitializeDatabase(SalesAndInventoryDbContext context)
        {
            if (!context.Database.CanConnect())
            {
                throw new Exception("Unable to connect to the database.");
            }

            var employee = new Employee(
                "Doe",
                "John",
                "Sales Representative",
                "Mr.",
                new DateTime(1980, 1, 1),
                new DateTime(2020, 1, 1),
                "123 Main St.",
                "New York",
                "NY",
                "10001",
                "USA",
                "555-555-1234",
                new Employee(
                    "Smith",
                    "Jane",
                    "Sales Manager",
                    "Ms.",
                    new DateTime(1975, 1, 1),
                    new DateTime(2015, 1, 1),
                    "456 Park Ave.",
                    "New York",
                    "NY",
                    "10002",
                    "USA",
                    "555-555-5678")
                );

            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}