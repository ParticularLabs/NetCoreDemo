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
        private readonly StockItemDbContext context;

        public StockItemsStatusController(StockItemDbContext context)
        {
            this.context = context;

            if (!context.StockItems.Any())
            {
                context.StockItems.Add(new StockItem {Id = 1, InStock = true, ProductId = 1});
                context.StockItems.Add(new StockItem {Id = 2, InStock = true, ProductId = 2});
                context.StockItems.Add(new StockItem {Id = 3, InStock = false, ProductId = 3});
                context.SaveChanges();
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(int id)
        {
            var item = context.StockItems.FirstOrDefault(t => t.ProductId == id);
            if (item == null) return NotFound();
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetById(string productIds)
        {
            if (productIds == null) return NotFound();
            var productIdList = productIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
            var productsList = context.StockItems.Where(p => productIdList.Contains(p.ProductId)).ToList();
            return new ObjectResult(productsList);
        }
    }
}