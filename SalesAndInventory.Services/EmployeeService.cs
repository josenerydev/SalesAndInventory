using SalesAndInventory.Models;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;
using SalesAndInventory.Shared.Services;

namespace SalesAndInventory.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerId(int managerId)
        {
            return await _unitOfWork.Employees.GetEmployeesByManagerId(managerId);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var addedEmployee = await _employeeRepository.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return addedEmployee;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            await _employeeRepository.RemoveAsync(employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
