﻿using Microsoft.AspNetCore.Mvc;

namespace EShop.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}