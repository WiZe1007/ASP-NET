using Microsoft.AspNetCore.Mvc;

namespace Task_13.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // За замовчуванням вибираємо "Каїр" для комбобоксу та листбоксу
            ViewBag.ComboCity = "Каїр";
            ViewBag.ListCity = "Каїр";
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            // Зчитуємо значення кнопки з форми через Request
            string action = Request.Form["action"];
            if (action == "reset")
            {
                // Якщо натиснули "Скинути" – повертаємо дефолтні значення
                ViewBag.ComboCity = "Каїр";
                ViewBag.ListCity = "Каїр";
                ViewBag.ShowResult = false;
            }
            else
            {
                // Інакше зчитуємо обрані значення з комбобоксу та листбоксу через Request
                string comboCity = Request.Form["comboCity"];
                string listCity = Request.Form["listCity"];
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
