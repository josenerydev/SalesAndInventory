using SalesAndInventory.Models;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;
using SalesAndInventory.Shared.Services;

namespace SalesAndInventory.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
