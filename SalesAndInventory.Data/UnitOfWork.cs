using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesAndInventoryDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(SalesAndInventoryDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using var context = _context;
            return await context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            using var context = _context;
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
