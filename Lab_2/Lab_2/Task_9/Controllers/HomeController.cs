using Microsoft.AspNetCore.Mvc;

namespace Task_9.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Задаємо дефолтні інтереси
            ViewBag.SelectedInterests = new List<string> { "Спорт", "Майстрування" };
            // Спочатку не показуємо результат
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(string action)
        {
            if (action == "cancel")
            {
                // Якщо користувач натиснув "Відмовитися" - повертаємо дефолтні значення
                ViewBag.SelectedInterests = new List<string> { "Спорт", "Майстрування" };
                ViewBag.ShowResult = false;
            }
            else
            {
                // Якщо натиснуто "Вибрати", зчитуємо вибрані чекбокси
                var selected = Request.Form["Interests"]; // Отримуємо дані з форми
                List<string> selectedList = selected.ToList(); // Перетворюємо в список

                ViewBag.SelectedInterests = selectedList;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
