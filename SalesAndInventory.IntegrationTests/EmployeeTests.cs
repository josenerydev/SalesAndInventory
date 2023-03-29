using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.IntegrationTests
{
    public class EmployeeTests : IClassFixture<TestEnvironmentFixture>
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTests(TestEnvironmentFixture fixture)
        {
            _context = fixture.Context;
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
            var newEmployee = new Employee("Doe", "Jane", "Developer", "Ms.", new DateTime(1985, 1, 1),
                new DateTime(2021, 1, 1), "456 Test Street", "Test City", "USA", "555-6789");
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            int empId = newEmployee.EmpId;

            // Act
            var employee = await _context.Employees.FindAsync(empId);

            // Assert
            Assert.NotNull(employee);
            Assert.Equal(empId, employee.EmpId);
        }

        [Fact]
        public async Task Can_Update_Employee()
        {
            // Arrange
            var newEmployee = new Employee("Smith", "John", "Developer", "Mr.", new DateTime(1980, 1, 1),
                new DateTime(2020, 1, 1), "123 Test Street", "Test City", "USA", "555-1234");
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            int empId = newEmployee.EmpId;
            var newCity = "New City";

            // Act
            var employee = await _context.Employees.FindAsync(empId);
            employee.UpdateCity(newCity);
            await _context.SaveChangesAsync();
            var updatedEmployee = await _context.Employees.FindAsync(empId);

            // Assert
            Assert.Equal(newCity, updatedEmployee.City);
        }

        [Fact]
        public async Task Can_Delete_Employee()
        {
            // Arrange
            var newEmployee = new Employee("Brown", "Mary", "Developer", "Mrs.", new DateTime(1983, 1, 1),
                new DateTime(2022, 1, 1), "789 Test Street", "Test City", "USA", "555-4321");
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            int empId = newEmployee.EmpId;

            // Act
            var employee = await _context.Employees.FindAsync(empId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var deletedEmployee = await _context.Employees.FindAsync(empId);

            // Assert
            Assert.Null(deletedEmployee);
        }
    }
}