namespace SalesAndInventory.Api.Models
{
    public class Product
    {
        private Product()
        { }

        public Product(string productName, Supplier supplier, Category category, decimal unitPrice, bool discontinued = false)
        {
            ProductName = productName;
            Supplier = supplier;
            SupplierId = supplier.SupplierId;
            Category = category;
            CategoryId = category.CategoryId;
            UnitPrice = unitPrice;
            Discontinued = discontinued;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public decimal UnitPrice { get; private set; }
        public bool Discontinued { get; private set; }
        public ICollection<OrderDetail> OrderDetails { get; private set; } = new HashSet<OrderDetail>();
    }
}