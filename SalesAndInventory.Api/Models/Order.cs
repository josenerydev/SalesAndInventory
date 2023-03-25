namespace SalesAndInventory.Api.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public int? CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public int EmployeeId { get; private set; }
        public Employee Employee { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime RequiredDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public int ShipperId { get; private set; }
        public Shipper Shipper { get; private set; }
        public decimal Freight { get; private set; }
        public string ShipName { get; private set; }
        public string ShipAddress { get; private set; }
        public string ShipCity { get; private set; }
        public string? ShipRegion { get; private set; }
        public string? ShipPostalCode { get; private set; }
        public string ShipCountry { get; private set; }

        private Order()
        { }

        public Order(int? customerId, Customer customer, int employeeId, Employee employee, DateTime orderDate, DateTime requiredDate, DateTime? shippedDate, Shipper shipper, decimal freight,
            string shipName, string shipAddress, string shipCity, string? shipRegion, string? shipPostalCode, string shipCountry)
        {
            CustomerId = customerId;
            Customer = customer;
            EmployeeId = employeeId;
            Employee = employee;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            Shipper = shipper;
            ShipperId = shipper.ShipperId;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
        }
    }
}