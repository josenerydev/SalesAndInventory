using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId);
    }
}