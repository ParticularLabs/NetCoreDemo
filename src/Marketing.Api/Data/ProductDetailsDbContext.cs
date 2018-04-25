namespace Marketing.Api.Data
{
    using Marketing.Api.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductDetailsDbContext : DbContext
    {
        public ProductDetailsDbContext(DbContextOptions<ProductDetailsDbContext> productDetails)
            : base(productDetails)
        {
        }

        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}