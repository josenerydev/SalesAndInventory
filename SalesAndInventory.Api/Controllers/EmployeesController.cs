using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Shared.Dtos;
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
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            return await _employeeService.GetAllEmployees();
        }
    }
}
