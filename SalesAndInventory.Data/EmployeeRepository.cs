using Microsoft.EntityFrameworkCore;
using SalesAndInventory.Models;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Data
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerId(int managerId)
        {
            return await _unitOfWork.Set<Employee>()
                .Where(e => e.ManagerId == managerId)
                .ToListAsync();
        }
    }
}
