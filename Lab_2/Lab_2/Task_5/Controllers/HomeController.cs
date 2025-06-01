using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Встановлюємо дефолтні значення для радіо-кнопок
            ViewBag.City = "Kharkiv";       // За замовчуванням – Харків
            ViewBag.Transport = "Train";     // За замовчуванням – Потяг
            ViewBag.ShowResult = false;      // Перший запуск – результат не показується
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(string action)
        {
            if (action == "cancel")
            {
                // Якщо натиснуто "Відмовитися", повертаємо дефолтний стан
                ViewBag.City = "Kharkiv";
                ViewBag.Transport = "Train";
                ViewBag.ShowResult = false; // Результат не показується
            }
            else
            {
                // Якщо натиснуто "Вибрати", зчитуємо вибір користувача з форми
                string city = Request.Form["City"];
                string transport = Request.Form["Transport"];

                // Зберігаємо отримані значення для подальшого відображення
                ViewBag.City = city;
                ViewBag.Transport = transport;
                ViewBag.ShowResult = true;  // Показуємо результат
            }

            return View();
        }
    }
}
