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

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerIdAsync(int managerId)
        {
            var employees = await _employeeRepository.GetEmployeesByManagerIdAsync(managerId);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee != null)
            {
                // Atualize apenas as propriedades que foram fornecidas no objeto employeeDto.
                if (employeeDto.LastName != null)
                    employee.LastName = employeeDto.LastName;

                if (employeeDto.FirstName != null)
                    employee.FirstName = employeeDto.FirstName;

                if (employeeDto.Title != null)
                    employee.Title = employeeDto.Title;

                if (employeeDto.TitleOfCourtesy != null)
                    employee.TitleOfCourtesy = employeeDto.TitleOfCourtesy;

                if (employeeDto.BirthDate.HasValue)
                    employee.BirthDate = employeeDto.BirthDate.Value;

                if (employeeDto.HireDate.HasValue)
                    employee.HireDate = employeeDto.HireDate.Value;

                if (employeeDto.Address != null)
                    employee.Address = employeeDto.Address;

                if (employeeDto.City != null)
                    employee.City = employeeDto.City;

                if (employeeDto.Region != null)
                    employee.Region = employeeDto.Region;

                if (employeeDto.PostalCode != null)
                    employee.PostalCode = employeeDto.PostalCode;

                if (employeeDto.Country != null)
                    employee.Country = employeeDto.Country;

                if (employeeDto.Phone != null)
                    employee.Phone = employeeDto.Phone;

                if (employeeDto.MgrId.HasValue)
                    employee.MgrId = employeeDto.MgrId;

                _employeeRepository.Update(employee);
                await _employeeRepository.SaveAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
                await _employeeRepository.SaveAsync();
            }
        }
    }
}