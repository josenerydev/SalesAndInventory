using SalesAndInventory.Models;

namespace SalesAndInventory.Shared.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
