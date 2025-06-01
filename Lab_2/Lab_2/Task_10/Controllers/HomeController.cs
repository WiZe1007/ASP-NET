using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Для роботи з IFormCollection
using System.Collections.Generic;
using System.Linq;

namespace Task_10.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Задаємо початкові значення: "Спорт" та "Майстрування"
            ViewBag.SelectedInterests = new List<string> { "Спорт", "Майстрування" };
            // Спочатку результат не показується
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(IFormCollection form, string action)
        {
            if (action == "cancel")
            {
                // Якщо користувач натиснув "Відмовитися", повертаємо дефолтні значення
                ViewBag.SelectedInterests = new List<string> { "Спорт", "Майстрування" };
                ViewBag.ShowResult = false;
            }
            else
            {
                // Якщо натиснуто "Вибрати", читаємо значення чекбоксів із форми
                var selected = form["Interests"]; // Отримуємо вибрані значення
                List<string> selectedList = selected.ToList(); // Перетворюємо їх у список

                ViewBag.SelectedInterests = selectedList;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
