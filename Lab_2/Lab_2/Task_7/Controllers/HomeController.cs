using Microsoft.AspNetCore.Mvc;

namespace Task_7.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // При першому завантаженні встановлюємо дефолтні значення
            ViewBag.City = "Kharkiv";
            ViewBag.Transport = "Train";
            ViewBag.ShowResult = false;
            return View();
        }

        // Параметри City, Transport та action отримуються безпосередньо з форми
        [HttpPost]
        public IActionResult Index(string City, string Transport, string action)
        {
            if (action == "cancel")
            {
                // Якщо натиснуто "Відмовитися", повертаємо дефолтний стан
                ViewBag.City = "Kharkiv";
                ViewBag.Transport = "Train";
                ViewBag.ShowResult = false;
            }
            else
            {
                // Якщо натиснуто "Вибрати", зберігаємо значення з форми для відображення
                ViewBag.City = City;
                ViewBag.Transport = Transport;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
