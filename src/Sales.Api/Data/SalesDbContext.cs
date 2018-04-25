namespace Sales.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using Sales.Api.Models;

    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> dbContext)
            : base(dbContext)
        {
        }

        public DbSet<ProductPrice> ProductPrices { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}