
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Models;

namespace Shipping.Api.Data
{
    public class StockItemDbContext : DbContext
    {
        public StockItemDbContext(DbContextOptions<StockItemDbContext> stockItem)
            : base(stockItem)
        {
        }

        public DbSet<StockItem> StockItems { get; set; }
    }
}
