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
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            return await _employeeService.GetAllEmployeesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            await _employeeService.AddEmployeeAsync(employeeDto);

            return CreatedAtAction(nameof(GetById), new { id = employeeDto.EmpId }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmpId)
            {
                return BadRequest();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(id);

            return NoContent();
        }
    }
}