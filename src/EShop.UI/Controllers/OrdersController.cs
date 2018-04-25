

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CancelOrder(int id)
        {
            return View();
        }
    }
}