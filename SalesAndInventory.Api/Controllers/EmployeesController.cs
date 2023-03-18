using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Models;
using SalesAndInventory.Shared.Services;

namespace SalesAndInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employeeService.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await _employeeService.GetEmployeeById(id);
        }

        [HttpGet("manager/{managerId}")]
        public async Task<IEnumerable<Employee>> GetByManagerId(int managerId)
        {
            return await _employeeService.GetEmployeesByManagerId(managerId);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            await _employeeService.UpdateEmployee(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployee(employee);

            return NoContent();
        }
    }
}
