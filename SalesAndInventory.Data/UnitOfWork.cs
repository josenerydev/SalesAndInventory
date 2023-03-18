using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesAndInventoryDbContext _context;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(SalesAndInventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(this);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
    }
}
