using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Services;
using SalesAndInventory.Api.Utilities;

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
        public async Task<ActionResult<Result<IEnumerable<SupplierDto>>>> Get()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(Result<IEnumerable<SupplierDto>>.Success(suppliers.Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<SupplierDto>>> GetById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound(Result<SupplierDto>.Failure("Supplier not found"));
            }

            return Ok(Result<SupplierDto>.Success(supplier.Data));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDto supplierDto)
        {
            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SupplierDto>.Failure(errors));
            }

            var result = await _supplierService.AddSupplierAsync(supplierDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = supplierDto.SupplierId }, Result<SupplierDto>.Success(supplierDto));
            }

            return BadRequest(Result<SupplierDto>.Failure(result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierDto supplierDto)
        {
            if (id != supplierDto.SupplierId)
            {
                return BadRequest(Result<SupplierDto>.Failure("Invalid supplier ID"));
            }

            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(Result<SupplierDto>.Failure(errors));
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound(Result<SupplierDto>.Failure("Supplier not found"));
            }

            var result = await _supplierService.UpdateSupplierAsync(id, supplierDto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(Result<SupplierDto>.Failure(result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound(Result<SupplierDto>.Failure("Supplier not found"));
            }

            var result = await _supplierService.DeleteSupplierAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(Result<SupplierDto>.Failure(result.Errors));
        }
    }
}