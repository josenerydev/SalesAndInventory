using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Models;
using System.Reflection;

namespace SalesAndInventory.Data
{
    public class SalesAndInventoryDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        // DbSet para outras entidades

        public SalesAndInventoryDbContext(DbContextOptions<SalesAndInventoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
