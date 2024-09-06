namespace ProductCategoryAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Make Products optional
        public List<Product>? Products { get; set; } = new List<Product>();  // Allow null or empty list
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
