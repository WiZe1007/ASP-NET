using Microsoft.AspNetCore.Mvc;
using Task_12.Models;

namespace Task_12.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Створюємо нову модель з дефолтними значеннями (Спорт та Майстрування увімкнені)
            var model = new InterestsModel();
            ViewBag.ShowResult = false; // На початку результат не показується
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(InterestsModel model, string action)
        {
            if (action == "cancel")
            {
                // Якщо користувач натиснув "Відмовитися" – повертаємо модель з початковими значеннями
                model = new InterestsModel();
                ViewBag.ShowResult = false;
                ModelState.Clear(); // Очищуємо ModelState, щоб скинути попередні дані
            }
            else
            {
                // Якщо вибір підтверджено, показуємо результати з моделі
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
