using SalesAndInventory.Models;

namespace SalesAndInventory.Shared.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByManagerId(int managerId);
    }
}
