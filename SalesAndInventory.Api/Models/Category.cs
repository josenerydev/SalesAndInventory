namespace SalesAndInventory.Api.Models
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public string Description { get; private set; }

        private Category()
        { }

        public Category(string categoryName, string description)
        {
            CategoryName = categoryName;
            Description = description;
        }
    }
}