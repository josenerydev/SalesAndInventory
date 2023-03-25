namespace SalesAndInventory.Api.Models
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int SupplierId { get; private set; }
        public int CategoryId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public bool Discontinued { get; private set; }

        public Supplier Supplier { get; private set; }
        public Category Category { get; private set; }

        private Product()
        { }

        public Product(string productName, int supplierId, int categoryId, decimal unitPrice, bool discontinued = false)
        {
            ProductName = productName;
            SupplierId = supplierId;
            CategoryId = categoryId;
            UnitPrice = unitPrice;
            Discontinued = discontinued;
        }
    }
}