using AutoMapper;
using FluentValidation;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;
using SalesAndInventory.Api.Repositories;
using SalesAndInventory.Api.Utilities;

namespace SalesAndInventory.Api.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<SupplierDto> _supplierValidator;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper, IValidator<SupplierDto> supplierValidator)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _supplierValidator = supplierValidator;
        }

        public async Task<Result<IEnumerable<SupplierDto>>> GetAllSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            return Result<IEnumerable<SupplierDto>>.Success(supplierDtos);
        }

        public async Task<Result<SupplierDto>> GetSupplierByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
            {
                return Result<SupplierDto>.Failure($"Supplier with ID {id} not found.");
            }

            var supplierDto = _mapper.Map<SupplierDto>(supplier);
            return Result<SupplierDto>.Success(supplierDto);
        }

        public async Task<Result<SupplierDto>> AddSupplierAsync(SupplierDto supplierDto)
        {
            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<SupplierDto>.Failure(errors);
            }

            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.AddAsync(supplier);
            await _supplierRepository.SaveAsync();

            supplierDto.SupplierId = supplier.SupplierId;
            return Result<SupplierDto>.Success(supplierDto);
        }

        public async Task<Result<SupplierDto>> UpdateSupplierAsync(int id, SupplierDto supplierDto)
        {
            var validationResult = _supplierValidator.Validate(supplierDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<SupplierDto>.Failure(errors);
            }

            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
            {
                return Result<SupplierDto>.Failure($"Supplier with ID {id} not found.");
            }

            _mapper.Map(supplierDto, supplier);
            _supplierRepository.Update(supplier);
            await _supplierRepository.SaveAsync();

            return Result<SupplierDto>.Success(supplierDto);
        }

        public async Task<Result<SupplierDto>> DeleteSupplierAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
            {
                return Result<SupplierDto>.Failure($"Supplier with ID {id} not found.");
            }
            var supplierDto = _mapper.Map<SupplierDto>(supplier);
            _supplierRepository.Delete(supplier);
            await _supplierRepository.SaveAsync();

            return Result<SupplierDto>.Success(supplierDto);
        }
    }
}