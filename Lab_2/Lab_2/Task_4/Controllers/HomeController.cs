using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        // GET-запит: повертаємо форму з новою (порожньою) моделлю
        [HttpGet]
        public IActionResult Index()
        {
            // Створюємо новий об'єкт PersonModel із початковими значеннями
            return View(new PersonModel());
        }

        // POST-запит: отримуємо дані з форми через строго типізовану модель
        [HttpPost]
        public IActionResult Index(PersonModel model, string action)
        {
            if (action == "reset")
            {
                // Якщо натиснуто "Скинути", очищуємо стан моделі та створюємо нову порожню модель
                ModelState.Clear(); // Очищення помилок валідації, якщо були
                model = new PersonModel();
            }
            // Повертаємо подання з отриманою або очищеною моделлю
            return View(model);
        }
    }
}
