namespace Marketing.Api.Data
{
    using Marketing.Api.Models;

    static class DataInitializer
    {
        public static void Initialize(ProductDetailsDbContext context)
        {
            context.ProductDetails.Add(new ProductDetails
            {
                ProductId = 1,
                Name = "Apple iPhone X",
                Description = "5.8-inch display, Space Gray, 256GB",
                ImageUrl =
                    "https://www.t-mobile.com/content/dam/t-mobile/en-p/cell-phones/apple/apple-iphone-x/silver/Apple-iPhoneX-Silver-1-3x.jpg"
            });

            context.ProductDetails.Add(new ProductDetails
            {
                ProductId = 2,
                Name = "Google Pixel 2 XL",
                Description = "6-inch display, Unlocked, Just Black, 128GB",
                ImageUrl = "https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6099/6099989_sd.jpg"
            });

            context.ProductDetails.Add(new ProductDetails
            {
                ProductId = 3,
                Name = "Galaxy Note 8",
                Description = "6.3-inch Infinity Display, Midnight Black, 64GB",
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81zc0eOl8RL._SY445_.jpg"
            });

            context.SaveChanges();

        }
    }
}