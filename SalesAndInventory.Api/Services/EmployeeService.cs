using AutoMapper;
using FluentValidation;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;
using SalesAndInventory.Api.Repositories;
using SalesAndInventory.Api.Utilities;

namespace SalesAndInventory.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeDto> _employeeValidator;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IValidator<EmployeeDto> employeeValidator)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeValidator = employeeValidator;
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Result<IEnumerable<EmployeeDto>>.Success(employeeDtos);
        }

        public async Task<Result<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return Result<EmployeeDto>.Failure($"Employee with ID {id} not found.");
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Result<EmployeeDto>.Success(employeeDto);
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesByManagerIdAsync(int managerId)
        {
            var employees = await _employeeRepository.GetEmployeesByManagerIdAsync(managerId);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Result<IEnumerable<EmployeeDto>>.Success(employeeDtos);
        }

        public async Task<Result<EmployeeDto>> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<EmployeeDto>.Failure(errors);
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();

            employeeDto.EmpId = employee.EmpId;
            return Result<EmployeeDto>.Success(employeeDto);
        }

        public async Task<Result<EmployeeDto>> UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<EmployeeDto>.Failure(errors);
            }

            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return Result<EmployeeDto>.Failure($"Employee with ID {id} not found.");
            }

            _mapper.Map(employeeDto, employee);
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();

            return Result<EmployeeDto>.Success(employeeDto);
        }

        public async Task<Result<EmployeeDto>> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return Result<EmployeeDto>.Failure($"Employee with ID {id} not found.");
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveAsync();

            return Result<EmployeeDto>.Success(employeeDto);
        }
    }
}