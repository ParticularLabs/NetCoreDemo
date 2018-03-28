using Microsoft.AspNetCore.Mvc;

namespace EShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult BuyItem(int id)
        {
            return View();
        }
    }
}