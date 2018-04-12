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
    }

}

