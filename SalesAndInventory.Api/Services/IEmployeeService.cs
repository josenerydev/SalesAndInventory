using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;
using SalesAndInventory.Api.Utilities;

namespace SalesAndInventory.Api.Services
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync();

        Task<Result<EmployeeDto>> GetEmployeeByIdAsync(int id);

        Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesByManagerIdAsync(int managerId);

        Task<Result<EmployeeDto>> AddEmployeeAsync(EmployeeDto employeeDto);

        Task<Result<EmployeeDto>> UpdateEmployeeAsync(int id, EmployeeDto employeeDto);

        Task<Result<EmployeeDto>> DeleteEmployeeAsync(int id);
    }
}