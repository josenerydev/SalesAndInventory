using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto> GetEmployeeByIdAsync(int id);

        Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerIdAsync(int managerId);

        Task AddEmployeeAsync(EmployeeDto employeeDto);

        Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto);

        Task DeleteEmployeeAsync(int id);
    }
}