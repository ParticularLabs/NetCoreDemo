using System;

namespace Sales.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sales.Api.Models;
    using System.Linq;
    using Sales.Api.Data;

    [Route("product")]
    public class ProductPriceController : Controller
    {
        private readonly SalesDbContext context;

        public ProductPriceController(SalesDbContext context)
        {
            this.context = context;

            if (!context.ProductPrices.Any())
            {
                context.ProductPrices.Add(new ProductPrice() { Id = 1, Price = new decimal(1095.00), ProductId = 1 });
                context.ProductPrices.Add(new ProductPrice() { Id = 2, Price = new decimal(949.00), ProductId = 2 });
                context.ProductPrices.Add(new ProductPrice() { Id = 3, Price = new decimal(950.00), ProductId = 3 });
                context.SaveChanges();
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long id)
        {
            var item = context.ProductPrices.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetById(string productIds)
        {
            if (productIds == null)
            {
                return NotFound();
            }
            var productIdList = productIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
            var productsList = context.ProductPrices.Where(p => productIdList.Contains(p.ProductId)).ToList();
            return new ObjectResult(productsList);
        }
    }

}

