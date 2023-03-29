namespace SalesAndInventory.Api.Models
{
    public class Order
    {
        private Order()
        { }

        public Order(Customer customer, Employee employee, Shipper shipper, DateTime orderDate, DateTime requiredDate,
                     string shipName, string shipAddress, string shipCity, string shipCountry, decimal freight,
                     DateTime? shippedDate = null, string shipRegion = null, string shipPostalCode = null)
        {
            Customer = customer;
            CustId = customer.CustId;
            Employee = employee;
            EmpId = employee.EmpId;
            Shipper = shipper;
            ShipperId = shipper.ShipperId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipCountry = shipCountry;
            Freight = freight;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
        }

        public int OrderId { get; private set; }
        public int? CustId { get; private set; }
        public Customer Customer { get; private set; }
        public int EmpId { get; private set; }
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
        public string ShipRegion { get; private set; }
        public string ShipPostalCode { get; private set; }
        public string ShipCountry { get; private set; }
        public ICollection<OrderDetail> OrderDetails { get; private set; } = new HashSet<OrderDetail>();
    }
}