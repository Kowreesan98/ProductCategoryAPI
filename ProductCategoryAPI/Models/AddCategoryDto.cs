namespace ProductCategoryAPI.Models
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
