﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
{
    public class Page1Controller : Controller
    {
        public IActionResult Index()
        {
            const string sessionKey = "Page1Count";
            int count = (HttpContext.Session.GetInt32(sessionKey) ?? 0) + 1;
            HttpContext.Session.SetInt32(sessionKey, count);
            ViewBag.VisitCount = count;
            return View();
        }
    }
}
