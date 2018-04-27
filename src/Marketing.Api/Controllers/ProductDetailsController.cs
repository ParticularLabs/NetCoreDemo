namespace Marketing.Api.Controllers
{
    using System;
    using System.Linq;
    using Marketing.Api.Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("product")]
    public class ProductDetailsController : Controller
    {
        readonly ProductDetailsDbContext context;

        public ProductDetailsController(ProductDetailsDbContext context)
        {
            this.context = context;
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

            var orderIdList = orderIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => s)
                .ToList();

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