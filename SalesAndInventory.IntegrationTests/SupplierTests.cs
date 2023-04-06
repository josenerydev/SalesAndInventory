using SalesAndInventory.Api.Models;
using Xunit;
using System;
using System.Threading.Tasks;
using SalesAndInventory.Api.Data;

namespace SalesAndInventory.IntegrationTests
{
    public class SupplierTests : IClassFixture<TestEnvironmentFixture>
    {
        private readonly ApplicationDbContext _context;

        public SupplierTests(TestEnvironmentFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task Can_Create_Supplier()
        {
            // Arrange
            var supplier = new Supplier("Test Company", "John Doe", "CEO", "123 Test Street", "Test City", "USA", "555-1234");

            // Act
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            // Assert
            Assert.NotEqual(0, supplier.SupplierId);
        }

        [Fact]
        public async Task Can_Read_Supplier()
        {
            // Arrange
            var newSupplier = new Supplier("Test Company 2", "Jane Doe", "CEO", "456 Test Street", "Test City", "USA", "555-6789");
            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();
            int supplierId = newSupplier.SupplierId;

            // Act
            var supplier = await _context.Suppliers.FindAsync(supplierId);

            // Assert
            Assert.NotNull(supplier);
            Assert.Equal(supplierId, supplier.SupplierId);
        }

        [Fact]
        public async Task Can_Update_Supplier()
        {
            // Arrange
            var newSupplier = new Supplier("Test Company 3", "John Smith", "CEO", "123 Test Street", "Test City", "USA", "555-1234");
            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();
            int supplierId = newSupplier.SupplierId;
            var newCity = "New City";

            // Act
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            supplier.GetType().GetProperty("City").SetValue(supplier, newCity);
            await _context.SaveChangesAsync();
            var updatedSupplier = await _context.Suppliers.FindAsync(supplierId);

            // Assert
            Assert.Equal(newCity, updatedSupplier.City);
        }

        [Fact]
        public async Task Can_Delete_Supplier()
        {
            // Arrange
            var newSupplier = new Supplier("Test Company 4", "Mary Brown", "CEO", "789 Test Street", "Test City", "USA", "555-4321");
            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();
            int supplierId = newSupplier.SupplierId;

            // Act
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            var deletedSupplier = await _context.Suppliers.FindAsync(supplierId);

            // Assert
            Assert.Null(deletedSupplier);
        }
    }
}