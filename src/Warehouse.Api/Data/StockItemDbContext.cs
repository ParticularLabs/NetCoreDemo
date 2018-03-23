
namespace Warehouse.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using Warehouse.Api.Models;

    public class StockItemDbContext : DbContext
    {
        public StockItemDbContext(DbContextOptions<StockItemDbContext> stockItem)
            : base(stockItem)
        {
        }

        public DbSet<StockItem> StockItems { get; set; }
    }
}
