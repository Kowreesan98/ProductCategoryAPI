using Microsoft.EntityFrameworkCore;
using ProductCategoryAPI.Models;

namespace ProductCategoryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {
        }
       
        public DbSet <Product> Product { get; set; }
        public DbSet <Category> Category { get; set; }
    }
}
