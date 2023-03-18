using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Models;
using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Data
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly SalesAndInventoryDbContext _dbContext;

        public EmployeeRepository(SalesAndInventoryDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerId(int managerId)
        {
            return await _dbContext.Employees.Where(e => e.ManagerId == managerId).ToListAsync();
        }
    }
}
