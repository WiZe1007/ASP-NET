using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("Login", "Account");

            ViewData["UserName"] = user;
            return View();
        }
    }
}
