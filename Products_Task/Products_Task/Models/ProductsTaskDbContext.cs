using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Products_Task.Models
{
    public class ProductsTaskDbContext:DbContext
    {
        public ProductsTaskDbContext()
        {

        }
        public ProductsTaskDbContext(DbContextOptions<ProductsTaskDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
