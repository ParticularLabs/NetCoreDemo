namespace Shipping.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shipping.Api.Models;

    public class StockItemDbContext : DbContext
    {
        public StockItemDbContext(DbContextOptions<StockItemDbContext> stockItem)
            : base(stockItem)
        {
        }

        public DbSet<StockItem> StockItems { get; set; }
    }
}