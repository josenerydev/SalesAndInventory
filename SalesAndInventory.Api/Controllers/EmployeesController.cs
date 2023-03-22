using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Services;
using SalesAndInventory.Api.Utilities;

namespace SalesAndInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeDto> _employeeValidator;

        public EmployeesController(IEmployeeService employeeService, IValidator<EmployeeDto> employeeValidator)
        {
            _employeeService = employeeService;
            _employeeValidator = employeeValidator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<EmployeeDto>>>> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(Result<IEnumerable<EmployeeDto>>.Success(employees.Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<EmployeeDto>>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(Result<EmployeeDto>.Failure("Employee not found"));
            }

            return Ok(Result<EmployeeDto>.Success(employee.Data));
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<EmployeeDto>.Failure(errors));
            }

            var result = await _employeeService.AddEmployeeAsync(employeeDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = employeeDto.EmpId }, Result<EmployeeDto>.Success(employeeDto));
            }

            return BadRequest(Result<EmployeeDto>.Failure(result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmpId)
            {
                return BadRequest(Result<EmployeeDto>.Failure("Invalid employee ID"));
            }

            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<EmployeeDto>.Failure(errors));
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(Result<EmployeeDto>.Failure("Employee not found"));
            }

            var result = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(Result<EmployeeDto>.Failure(result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(Result<EmployeeDto>.Failure("Employee not found"));
            }

            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(Result<EmployeeDto>.Failure(result.Errors));
        }
    }
}