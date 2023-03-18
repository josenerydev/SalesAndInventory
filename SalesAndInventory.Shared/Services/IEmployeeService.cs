using SalesAndInventory.Models;
using SalesAndInventory.Shared.Dtos;

namespace SalesAndInventory.Shared.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
    }
}
