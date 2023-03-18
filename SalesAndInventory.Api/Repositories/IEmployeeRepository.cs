using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(int id);

        Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId);

        Task CreateAsync(Employee entity);

        Task UpdateAsync(Employee entity);

        Task DeleteAsync(int id);
    }
}