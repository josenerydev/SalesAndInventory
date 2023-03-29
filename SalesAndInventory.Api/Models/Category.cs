namespace SalesAndInventory.Api.Models
{
    public class Category
    {
        private Category()
        { }

        public Category(string categoryName, string description)
        {
            CategoryName = categoryName;
            Description = description;
        }

        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public string Description { get; private set; }
        public ICollection<Product> Products { get; private set; } = new HashSet<Product>();
    }
}