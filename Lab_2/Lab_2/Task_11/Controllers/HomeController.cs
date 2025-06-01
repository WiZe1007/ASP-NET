using Microsoft.AspNetCore.Mvc;

namespace Task_11.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Встановлюємо початкові значення: Спорт та Майстрування увімкнені
            ViewBag.Sport = true;
            ViewBag.Travel = false;
            ViewBag.Craft = true;
            ViewBag.Draw = false;

            ViewBag.ShowResult = false; // На старті не показуємо результат
            return View();
        }

        [HttpPost]
        public IActionResult Index(bool Sport, bool Travel, bool Craft, bool Draw, string action)
        {
            if (action == "cancel")
            {
                // Якщо користувач натиснув "Відмовитися", повертаємо початкові налаштування
                ViewBag.Sport = true;
                ViewBag.Travel = false;
                ViewBag.Craft = true;
                ViewBag.Draw = false;
                ViewBag.ShowResult = false;
            }
            else
            {
                // Якщо натиснуто "Вибрати", приймаємо дані з форми
                ViewBag.Sport = Sport;
                ViewBag.Travel = Travel;
                ViewBag.Craft = Craft;
                ViewBag.Draw = Draw;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
