namespace Sales.Api.Data
{
    using Sales.Api.Models;

    static class DataInitializer
    {
        public static void Initialize(SalesDbContext context)
        {
            context.ProductPrices.Add(new ProductPrice {Id = 1, Price = new decimal (1095.00), ProductId = 1});
            context.ProductPrices.Add(new ProductPrice {Id = 2, Price = new decimal (949.00), ProductId = 2});
            context.ProductPrices.Add(new ProductPrice {Id = 3, Price = new decimal (950.00), ProductId = 3});
            context.SaveChanges();
        }
    }
}