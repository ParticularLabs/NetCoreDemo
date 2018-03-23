

namespace Marketing.Api.Controllers
{
    using System.Linq;
    using Marketing.Api.Data;
    using Marketing.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("product")]
    public class TodoController : Controller
    {
        private readonly ProductDetailsDbContext context;

        public TodoController(ProductDetailsDbContext context)
        {
            this.context = context;

            if (!context.ProductDetails.Any())
            {
                context.ProductDetails.Add(new ProductDetails() { Description = "Coolest phone ever!! at least according to some!", Id = 1, Name = "iPhoneX", ProductId = 1 });
                context.SaveChanges();
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long id)
        {
            var item = context.ProductDetails.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}
