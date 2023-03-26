using System.Collections.Generic;

namespace SalesAndInventory.Api.Models
{
    public class Shipper
    {
        private Shipper()
        { }

        public Shipper(string companyName, string phone)
        {
            CompanyName = companyName;
            Phone = phone;
        }

        public int ShipperId { get; private set; }
        public string CompanyName { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Order> Orders { get; private set; } = new HashSet<Order>();
    }
}