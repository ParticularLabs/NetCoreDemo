using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleEShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}