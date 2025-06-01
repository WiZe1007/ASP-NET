using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - відображаємо порожню форму
        [HttpGet]
        public IActionResult Index()
        {
            return View(new VisitorViewModel());
        }

        // POST: /Home/Index - отримуємо дані з форми та повертаємо їх у вигляді моделі
        [HttpPost]
        public IActionResult Index(VisitorViewModel model)
        {
            // За потреби тут можна додати перевірку ModelState.IsValid
            return View(model);
        }
    }
}
