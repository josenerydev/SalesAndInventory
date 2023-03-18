namespace SalesAndInventory.Api.Models
{
    public class Employee
    {
        public int Id { get; private set; }
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
        public virtual Employee Manager { get; private set; }

        protected Employee()
        {
            // Required by EF Core
        }

        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate,
                        string address, string city, string region, string postalCode, string country, string phone, Employee manager = null)
        {
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
            Manager = manager;
            ManagerId = manager?.Id;
        }

        public void Update(Employee updatedEmployee)
        {
            FirstName = updatedEmployee.FirstName ?? FirstName;
            LastName = updatedEmployee.LastName ?? LastName;
            Title = updatedEmployee.Title ?? Title;
            TitleOfCourtesy = updatedEmployee.TitleOfCourtesy ?? TitleOfCourtesy;
            BirthDate = updatedEmployee.BirthDate;
            HireDate = updatedEmployee.HireDate;
            Address = updatedEmployee.Address ?? Address;
            City = updatedEmployee.City ?? City;
            Region = updatedEmployee.Region ?? Region;
            PostalCode = updatedEmployee.PostalCode ?? PostalCode;
            Country = updatedEmployee.Country ?? Country;
            Phone = updatedEmployee.Phone ?? Phone;
            ManagerId = updatedEmployee?.ManagerId ?? ManagerId;
        }
    }
}