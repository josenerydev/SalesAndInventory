using System;
using System.Collections.Generic;

namespace SalesAndInventory.Api.Models
{
    public class Customer
    {
        private Customer()
        { }

        public Customer(string companyName, string contactName, string contactTitle, string address, string city,
                        string country, string phone, string region = null, string postalCode = null, string fax = null)
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

        public int CustId { get; private set; }
        public string CompanyName { get; private set; }
        public string ContactName { get; private set; }
        public string ContactTitle { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public string Fax { get; private set; }
        public ICollection<Order> Orders { get; private set; } = new HashSet<Order>();
    }
}