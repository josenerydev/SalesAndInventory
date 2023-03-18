using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Shared.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
