using Microsoft.AspNetCore.Mvc;
using Task_16.Models;

namespace Task_16.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Створюємо модель з дефолтними значеннями ("Каїр" для обох)
            var model = new CitySelectionModel();
            ViewBag.ShowResult = false;
            return View(model);
        }

        // POST: /Home/Index
        // Сувора типізація – дані форми прив'язані до властивостей моделі
        [HttpPost]
        public IActionResult Index(CitySelectionModel model, string action)
        {
            if (action == "reset")
            {
                // Якщо натиснуто "Скинути" – повертаємо дефолтну модель і очищаємо ModelState
                model = new CitySelectionModel();
                ViewBag.ShowResult = false;
                ModelState.Clear();  // Очищуємо ModelState, щоб не було попередніх помилок
            }
            else
            {
                // Якщо натиснуто "Відіслати", приймаємо модель з форми
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
