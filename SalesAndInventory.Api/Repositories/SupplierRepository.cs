using SalesAndInventory.Api.Data;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Api.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implemente os métodos específicos para SupplierRepository, se necessário
    }
}