namespace Shipping.Api.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Shipping.Api.Data;
    using Shipping.Api.Models;

    [Route("product")]
    public class StockItemsStatusController : Controller
    {
        readonly StockItemDbContext context;

        public StockItemsStatusController(StockItemDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var item = context.StockItems.FirstOrDefault(t => t.ProductId == id);
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

            var productIdList = productIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
            var productsList = context.StockItems.Where(p => productIdList.Contains(p.ProductId)).ToList();
            return new ObjectResult(productsList);
        }
    }
}