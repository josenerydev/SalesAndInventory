using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class Employee
    {
        public int EmpId { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Title { get; private set; }
        public string TitleOfCourtesy { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime HireDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string? Region { get; private set; }
        public string? PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public int? ManagerId { get; private set; }
        public Employee Manager { get; private set; }
        public IReadOnlyCollection<Employee> Subordinates => _subordinates.AsReadOnly();

        private readonly List<Employee> _subordinates = new List<Employee>();

        private Employee()
        {
        }

        public Employee(int empId, string lastName, string firstName, string title, string titleOfCourtesy,
        DateTime birthDate, DateTime hireDate, string address, string city, string? region,
        string? postalCode, string country, string phone, int? managerId, Employee manager, IEnumerable<Employee> subordinates)
            : this()
        {
            EmpId = empId;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            ManagerId = managerId;
            Manager = manager;
            _subordinates.AddRange(subordinates);

            var validator = new EmployeeValidator();
            validator.ValidateAndThrow(this);
        }

        public void Promote(string newTitle, Employee newManager)
        {
            if (string.IsNullOrEmpty(newTitle))
            {
                throw new ArgumentException("The new title cannot be null or empty.", nameof(newTitle));
            }

            if (newManager == null)
            {
                throw new ArgumentNullException(nameof(newManager), "The new manager cannot be null.");
            }

            if (EmpId == newManager.EmpId)
            {
                throw new InvalidOperationException("An employee cannot be their own manager.");
            }

            Title = newTitle;
            SetManager(newManager);
        }

        public void SetManager(Employee manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), "The manager cannot be null.");
            }

            if (EmpId == manager.EmpId)
            {
                throw new InvalidOperationException("An employee cannot be their own manager.");
            }

            Manager = manager;
            ManagerId = manager.EmpId;
        }

        public void AddSubordinate(Employee subordinate)
        {
            if (subordinate == null)
            {
                throw new ArgumentNullException(nameof(subordinate), "Subordinate cannot be null.");
            }

            if (_subordinates.Any(x => x.EmpId == subordinate.EmpId))
            {
                throw new InvalidOperationException("Employee is already a subordinate.");
            }

            if (EmpId == subordinate.EmpId)
            {
                throw new InvalidOperationException("An employee cannot be their own subordinate.");
            }

            _subordinates.Add(subordinate);
            subordinate.ManagerId = EmpId;
            subordinate.Manager = this;
        }

        public void RemoveSubordinate(Employee subordinate)
        {
            if (subordinate == null)
            {
                throw new ArgumentNullException(nameof(subordinate), "Subordinate cannot be null.");
            }

            if (!_subordinates.Any(x => x.EmpId == subordinate.EmpId))
            {
                throw new InvalidOperationException("Employee is not a subordinate.");
            }

            _subordinates.Remove(subordinate);
            subordinate.ManagerId = null;
            subordinate.Manager = null;
        }
    }
}