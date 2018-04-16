using System;

namespace Marketing.Api.Controllers
{
    using System.Linq;
    using Marketing.Api.Data;
    using Marketing.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("product")]
    public class ProductDetailsController : Controller
    {
        private readonly ProductDetailsDbContext context;

        public ProductDetailsController(ProductDetailsDbContext context)
        {
            this.context = context;

            if (!context.ProductDetails.Any())
            {
                context.ProductDetails.Add(new ProductDetails
                {
                    ProductId = 1,
                    Name = "Apple iPhone X",
                    Description = "5.8-inch display, Space Gray, 256GB",
                    ImageUrl = "https://www.t-mobile.com/content/dam/t-mobile/en-p/cell-phones/apple/apple-iphone-x/silver/Apple-iPhoneX-Silver-1-3x.jpg"
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

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var item = context.ProductDetails.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        [Route("order")]
        public IActionResult GetByOrderIds(string orderIds)
        {
            if (orderIds == null)
            {
                return NotFound();
            }

            var orderIdList = orderIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => s).ToList();

            var productsList = context.OrderDetails.Where(order => orderIdList.Contains(order.OrderId))
                .Select(order => order.ProductId).Distinct().ToList();
            var productDetails = context.ProductDetails.Where(product => productsList.Contains(product.ProductId));
            return new ObjectResult(productDetails);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var item = context.ProductDetails.Take(10).ToList();
            return new ObjectResult(item);
        }
    }
}
