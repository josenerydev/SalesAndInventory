using SalesAndInventory.Api.Dtos;

namespace SalesAndInventory.Api.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();

        Task<EmployeeDto> GetEmployeeById(int id);

        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);

        Task<EmployeeDto> UpdateEmployee(int id, EmployeeDto employeeDto);

        Task<bool> DeleteEmployee(int id);

        Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerId(int managerId);
    }
}