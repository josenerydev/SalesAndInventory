namespace SalesAndInventory.Models
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
        public Employee Manager { get; private set; }

        private Employee() { }

        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate,
                        DateTime hireDate, string address, string city, string region, string postalCode, string country,
                        string phone, Employee manager = null)
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
            if (manager != null)
                ManagerId = manager.Id;

            if (!IsValid())
                throw new ArgumentException("Employee data is invalid.");
        }
        private bool IsValid()
        {
            if (string.IsNullOrEmpty(LastName) || LastName.Length > 20)
                throw new ArgumentException("Last name must not be empty and must be 20 characters or less.", nameof(LastName));

            if (string.IsNullOrEmpty(FirstName) || FirstName.Length > 10)
                throw new ArgumentException("First name must not be empty and must be 10 characters or less.", nameof(FirstName));

            if (string.IsNullOrEmpty(Title) || Title.Length > 30)
                throw new ArgumentException("Title must not be empty and must be 30 characters or less.", nameof(Title));

            if (string.IsNullOrEmpty(TitleOfCourtesy) || TitleOfCourtesy.Length > 25)
                throw new ArgumentException("Title of courtesy must not be empty and must be 25 characters or less.", nameof(TitleOfCourtesy));

            if (BirthDate > DateTime.Now.Date)
                throw new ArgumentException("Birth date must not be in the future.", nameof(BirthDate));

            if (HireDate > DateTime.Now.Date)
                throw new ArgumentException("Hire date must not be in the future.", nameof(HireDate));

            if (string.IsNullOrEmpty(Address) || Address.Length > 60)
                throw new ArgumentException("Address must not be empty and must be 60 characters or less.", nameof(Address));

            if (string.IsNullOrEmpty(City) || City.Length > 15)
                throw new ArgumentException("City must not be empty and must be 15 characters or less.", nameof(City));

            if (!string.IsNullOrEmpty(Region) && Region.Length > 15)
                throw new ArgumentException("Region must be 15 characters or less.", nameof(Region));

            if (!string.IsNullOrEmpty(PostalCode) && PostalCode.Length > 10)
                throw new ArgumentException("Postal code must be 10 characters or less.", nameof(PostalCode));

            if (string.IsNullOrEmpty(Country) || Country.Length > 15)
                throw new ArgumentException("Country must not be empty and must be 15 characters or less.", nameof(Country));

            if (string.IsNullOrEmpty(Phone) || Phone.Length > 24)
                throw new ArgumentException("Phone must not be empty and must be 24 characters or less.", nameof(Phone));

            if (BirthDate > DateTime.Now.Date)
                throw new ArgumentException("BirthDate cannot be later than the current date.");

            return true;
        }
    }
}
