using Microsoft.AspNetCore.Mvc;

namespace Task_15.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // За замовчуванням встановлюємо "Каїр" для комбобоксу та листбоксу
            ViewBag.ComboCity = "Каїр";
            ViewBag.ListCity = "Каїр";
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        // Слабка типізація: параметри форми приймаються як рядки
        [HttpPost]
        public IActionResult Index(string comboCity, string listCity, string action)
        {
            if (action == "reset")
            {
                // Якщо натиснуто "Скинути", повертаємо значення за замовчуванням
                ViewBag.ComboCity = "Каїр";
                ViewBag.ListCity = "Каїр";
                ViewBag.ShowResult = false;
            }
            else
            {
                // Інакше зчитуємо вибрані значення з форми
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
