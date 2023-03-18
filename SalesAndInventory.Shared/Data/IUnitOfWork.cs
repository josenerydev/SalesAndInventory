using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Shared.Repositories;
using System.Collections.Generic;

namespace SalesAndInventory.Shared.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
