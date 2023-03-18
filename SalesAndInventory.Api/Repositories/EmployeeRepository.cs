using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Api.Models;
using SalesAndInventory.Api.Repositories;

namespace SalesAndInventory.Api.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SalesAndInventoryDbContext _dbContext;

        public EmployeeRepository(SalesAndInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.AsNoTracking().DefaultIfEmpty().ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return;
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId)
        {
            return await _dbContext.Employees.Where(e => e.ManagerId == managerId).ToListAsync();
        }
    }
}