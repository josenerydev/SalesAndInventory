using AutoMapper;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;
using SalesAndInventory.Api.Repositories;

namespace SalesAndInventory.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.CreateAsync(employee);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);

            Employee? manager = employeeDto.ManagerId != null
                ? await _employeeRepository.GetByIdAsync(employeeDto.ManagerId.Value)
                : null;

            if (existingEmployee == null)
            {
                return null!; // Or throw an exception if you prefer
            }

            var updatedEmployee = new Employee(
                     employeeDto.LastName,
                     employeeDto.FirstName,
                     employeeDto.Title,
                     employeeDto.TitleOfCourtesy,
                     employeeDto.BirthDate,
                     employeeDto.HireDate,
                     employeeDto.Address,
                     employeeDto.City,
                     employeeDto.Region,
                     employeeDto.PostalCode,
                     employeeDto.Country,
                     employeeDto.Phone,
                     manager
                );

            existingEmployee.Update(updatedEmployee);

            await _employeeRepository.UpdateAsync(existingEmployee);

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return false; // Or throw an exception if you prefer
            }

            await _employeeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerId(int managerId)
        {
            var employees = await _employeeRepository.GetEmployeesByManagerIdAsync(managerId);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
}