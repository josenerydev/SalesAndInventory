using FluentValidation.TestHelper;
using SalesAndInventory.Api.Models;

namespace SalesAndInventory.Tests
{
    public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator _validator;

        public EmployeeValidatorTests()
        {
            _validator = new EmployeeValidator();
        }

        [Fact]
        public void Should_HaveError_When_ManagerId_IsSameAs_EmpId()
        {
            var employee = CreateEmployee(empId: 1, managerId: 2); // Crie um Employee válido para começar
            employee.SetManager(employee); // Tente atribuir o funcionário como seu próprio gerente

            var result = _validator.TestValidate(employee);
            result.ShouldHaveValidationErrorFor(x => x.ManagerId);
        }

        [Fact]
        public void Should_HaveError_When_Employee_IsTheirOwnSubordinate()
        {
            var employee = CreateEmployee(empId: 1);
            var employeeWithSubordinates = CreateEmployeeWithSubordinates(empId: 2, subordinates: new[] { employee });

            employeeWithSubordinates.AddSubordinate(employeeWithSubordinates);

            var result = _validator.TestValidate(employeeWithSubordinates);
            result.ShouldHaveValidationErrorFor(x => x.Subordinates);
        }

        private Employee CreateEmployee(
            int empId,
            string lastName = "Doe",
            string firstName = "John",
            string title = "Developer",
            string titleOfCourtesy = "Mr.",
            DateTime? birthDate = null,
            DateTime? hireDate = null,
            string address = "123 Street",
            string city = "New York",
            string region = null,
            string postalCode = null,
            string country = "USA",
            string phone = "123456789",
            int? managerId = null,
            Employee manager = null)
        {
            if (birthDate == null) birthDate = DateTime.Now.AddYears(-30);
            if (hireDate == null) hireDate = DateTime.Now.AddYears(-1);
            var subordinates = new List<Employee>();
            return new Employee(empId, lastName, firstName, title, titleOfCourtesy, birthDate.Value, hireDate.Value, address, city, region, postalCode, country, phone, managerId, manager, subordinates);
        }

        private Employee CreateEmployeeWithSubordinates(
            int empId,
            IEnumerable<Employee> subordinates,
            string lastName = "Doe",
            string firstName = "John",
            string title = "Developer",
            string titleOfCourtesy = "Mr.",
            DateTime? birthDate = null,
            DateTime? hireDate = null,
            string address = "123 Street",
            string city = "New York",
            string region = null,
            string postalCode = null,
            string country = "USA",
            string phone = "123456789",
            int? managerId = null,
            Employee manager = null)
        {
            return new Employee(empId, lastName, firstName, title, titleOfCourtesy,
                birthDate ?? new DateTime(1980, 1, 1), hireDate ?? DateTime.Now, address, city, region, postalCode, country, phone, managerId, manager, subordinates);
        }
    }
}