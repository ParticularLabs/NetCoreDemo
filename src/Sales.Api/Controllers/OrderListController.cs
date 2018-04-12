namespace Sales.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sales.Api.Models;
    using System.Linq;
    using Sales.Api.Data;

    [Route("order")]
    public class OrderListController : Controller
    {
        private readonly SalesDbContext context;

        public OrderListController(SalesDbContext context)
        {
            this.context = context;
        }

        public IActionResult Get()
        {
            var item = context.OrderDetails.ToList();
            return new ObjectResult(item);
        }
    }

}

