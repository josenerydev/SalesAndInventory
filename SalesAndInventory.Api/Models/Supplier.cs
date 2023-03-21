namespace SalesAndInventory.Api.Models
{
    public class Supplier
    {
        public int SupplierId { get; private set; }
        public string CompanyName { get; private set; }
        public string ContactName { get; private set; }
        public string ContactTitle { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string? Region { get; private set; }
        public string? PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public string? Fax { get; private set; }

        private Supplier()
        { }

        public Supplier(string companyName, string contactName, string contactTitle, string address, string city, string country, string phone,
                        string region = null, string postalCode = null, string fax = null)
        {
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            City = city;
            Country = country;
            Phone = phone;
            Region = region;
            PostalCode = postalCode;
            Fax = fax;
        }
    }
}