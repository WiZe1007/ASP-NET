using Microsoft.AspNetCore.Mvc;
using Task_5.Models;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - відображаємо порожню форму для заповнення
        [HttpGet]
        public IActionResult Index()
        {
            return View(new VisitorViewModel());
        }

        // POST: /Home/Index - обробляємо дані з форми і повертаємо їх у вигляді моделі
        [HttpPost]
        public IActionResult Index(VisitorViewModel model)
        {
            // Можна додати перевірку ModelState.IsValid, якщо потрібно
            return View(model);
        }
    }
}
