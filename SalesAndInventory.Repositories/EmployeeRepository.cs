using SalesAndInventory.Data;
using SalesAndInventory.Models;
using SalesAndInventory.Shared.Data;
using SalesAndInventory.Shared.Repositories;

namespace SalesAndInventory.Repositories
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
            return await _unitOfWork.Employees.GetEmployeesByManagerId(managerId);
        }
    }
}
