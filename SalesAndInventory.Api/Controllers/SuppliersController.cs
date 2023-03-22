using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Services;

namespace SalesAndInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IValidator<SupplierDto> _supplierValidator;

        public SuppliersController(ISupplierService supplierService, IValidator<SupplierDto> supplierValidator)
        {
            _supplierService = supplierService;
            _supplierValidator = supplierValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> Get()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDto supplierDto)
        {
            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(errors);
            }

            var result = await _supplierService.AddSupplierAsync(supplierDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = supplierDto.SupplierId }, supplierDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierDto supplierDto)
        {
            if (id != supplierDto.SupplierId)
            {
                return BadRequest();
            }

            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(errors);
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var result = await _supplierService.UpdateSupplierAsync(id, supplierDto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var result = await _supplierService.DeleteSupplierAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }
    }
}