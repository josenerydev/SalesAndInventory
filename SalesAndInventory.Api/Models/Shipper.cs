namespace SalesAndInventory.Api.Models
{
    public class Shipper
    {
        public int ShipperId { get; private set; }
        public string CompanyName { get; private set; }
        public string Phone { get; private set; }

        private Shipper()
        { }

        public Shipper(string companyName, string phone)
        {
            CompanyName = companyName;
            Phone = phone;
        }
    }
}