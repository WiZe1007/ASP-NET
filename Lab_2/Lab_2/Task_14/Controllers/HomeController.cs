using Microsoft.AspNetCore.Http; // Для роботи з IFormCollection
using Microsoft.AspNetCore.Mvc;

namespace Task_14.Controllers
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
        public IActionResult Index(IFormCollection form)
        {
            // Зчитуємо дію, натиснуту у формі (choose або reset)
            string action = form["action"];
            if (action == "reset")
            {
                // Якщо натиснули "Скинути", повертаємо дефолтні значення
                ViewBag.ComboCity = "Каїр";
                ViewBag.ListCity = "Каїр";
                ViewBag.ShowResult = false;
            }
            else
            {
                // Якщо натиснули "Відіслати", читаємо значення з комбобоксу та листбоксу
                string comboCity = form["comboCity"];
                string listCity = form["listCity"];
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
