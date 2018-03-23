namespace Warehouse.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Warehouse.Api.Models;
    using System.Linq;
    using Warehouse.Api.Data;

    [Route("product")]
    public class StockItemsStatusController : Controller
    {
        private readonly StockItemDbContext context;

        public StockItemsStatusController(StockItemDbContext context)
        {
            this.context = context;

            if (!context.StockItems.Any())
            {
                context.StockItems.Add(new StockItem() { Id = 1, InStock = true, ProductId = 1 });
                context.SaveChanges();
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long id)
        {
            var item = context.StockItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}

