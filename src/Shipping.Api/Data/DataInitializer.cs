namespace Shipping.Api.Data
{
    using Shipping.Api.Models;

    static class DataInitializer
    {
        public static void Initialize(StockItemDbContext context)
        {
            context.StockItems.Add(new StockItem { Id = 1, InStock = true, ProductId = 1 });
            context.StockItems.Add(new StockItem { Id = 2, InStock = true, ProductId = 2 });
            context.StockItems.Add(new StockItem { Id = 3, InStock = false, ProductId = 3 });
            context.SaveChanges();
        }
    }
}