namespace SalesAndInventory.Api.Models
{
    public class Employee
    {
        private Employee()
        { }

        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate,
                        string address, string city, string country, string phone, string region = null, string postalCode = null, Employee manager = null)
        {
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Country = country;
            Phone = phone;
            Region = region;
            PostalCode = postalCode;
            Manager = manager;
            ManagerId = manager?.EmpId;
        }

        public int EmpId { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Title { get; private set; }
        public string TitleOfCourtesy { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime HireDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public int? ManagerId { get; private set; }
        public Employee Manager { get; private set; }
        public ICollection<Employee> Subordinates { get; private set; } = new HashSet<Employee>();
        public ICollection<Order> Orders { get; private set; } = new HashSet<Order>();

        public void UpdateCity(string city)
        {
            City = city;
        }
    }
}