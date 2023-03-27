using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Models;
using Xunit;

namespace SalesAndInventory.IntegrationTests
{
    public class EmployeeTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SalesAndInventoryTest;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Can_Create_Employee()
        {
            // Arrange
            var employee = new Employee("Doe", "John", "Developer", "Mr.", new DateTime(1980, 1, 1),
                new DateTime(2020, 1, 1), "123 Test Street", "Test City", "USA", "555-1234");

            // Act
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Assert
            Assert.NotEqual(0, employee.EmpId);
        }

        [Fact]
        public async Task Can_Read_Employee()
        {
            // Arrange
            var employee = new Employee("Smith", "Jane", "Manager", "Ms.", new DateTime(1975, 6, 15),
                new DateTime(2019, 5, 1), "456 Main Street", "Another City", "USA", "555-5678");
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Act
            var foundEmployee = await _context.Employees.FindAsync(employee.EmpId);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employee.FirstName, foundEmployee.FirstName);
        }

        [Fact]
        public async Task Can_Update_Employee()
        {
            // Arrange
            var employee = new Employee("Brown", "Alice", "Developer", "Mrs.", new DateTime(1985, 4, 20),
                new DateTime(2021, 3, 15), "789 Test Avenue", "Test City", "USA", "555-2468");
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Act
            employee = _context.Employees.Single(e => e.EmpId == employee.EmpId);
            employee.UpdateCity("Updated City");
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Assert
            var updatedEmployee = await _context.Employees.FindAsync(employee.EmpId);
            Assert.Equal("Updated City", updatedEmployee.City);
        }

        [Fact]
        public async Task Can_Delete_Employee()
        {
            // Arrange
            var employee = new Employee("Martin", "Bob", "Sales", "Mr.", new DateTime(1970, 8, 30),
                new DateTime(2018, 10, 10), "321 Sample Street", "Sample City", "USA", "555-1598");
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Act
            var employeeToDelete = await _context.Employees.FindAsync(employee.EmpId);
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();

            // Assert
            var deletedEmployee = await _context.Employees.FindAsync(employee.EmpId);
            Assert.Null(deletedEmployee);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}