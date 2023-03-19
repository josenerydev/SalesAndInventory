using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Services;

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
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(errors);
            }

            var result = await _employeeService.AddEmployeeAsync(employeeDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = employeeDto.EmpId }, employeeDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmpId)
            {
                return BadRequest();
            }

            var validationResult = _employeeValidator.Validate(employeeDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(errors);
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var result = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }
    }
}