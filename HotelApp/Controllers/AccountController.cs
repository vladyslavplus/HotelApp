﻿using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
