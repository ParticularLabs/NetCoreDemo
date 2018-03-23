
namespace Sales.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using Sales.Api.Models;

    public class ProductPriceDbContext : DbContext
    {
        public ProductPriceDbContext(DbContextOptions<ProductPriceDbContext> productDetails)
            : base(productDetails)
        {
        }

        public DbSet<ProductPrice> ProductPrices { get; set; }

    }
}
