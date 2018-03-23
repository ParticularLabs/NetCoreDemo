namespace Sales.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sales.Api.Models;
    using System.Linq;
    using Sales.Api.Data;

    [Route("product")]
    public class ProductPriceController : Controller
    {
        private readonly ProductPriceDbContext context;

        public ProductPriceController(ProductPriceDbContext context)
        {
            this.context = context;

            if (!context.ProductPrices.Any())
            {
                context.ProductPrices.Add(new ProductPrice() { Id = 1, Price = new decimal(1095.00), ProductId = 1 });
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

