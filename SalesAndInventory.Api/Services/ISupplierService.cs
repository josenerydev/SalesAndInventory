using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Utilities;

namespace SalesAndInventory.Api.Services
{
    public interface ISupplierService
    {
        Task<Result<IEnumerable<SupplierDto>>> GetAllSuppliersAsync();

        Task<Result<SupplierDto>> GetSupplierByIdAsync(int id);

        Task<Result<SupplierDto>> AddSupplierAsync(SupplierDto supplierDto);

        Task<Result<SupplierDto>> UpdateSupplierAsync(int id, SupplierDto supplierDto);

        Task<Result<SupplierDto>> DeleteSupplierAsync(int id);
    }
}