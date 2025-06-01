using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Потрібно для IFormCollection

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        // Обробка GET-запиту для сторінки
        [HttpGet]
        public IActionResult Index()
        {
            // Просто повертаємо View без даних
            return View();
        }

        // Обробка POST-запиту з форми
        [HttpPost]
        public IActionResult Index(IFormCollection form, string action)
        {
            if (action == "reset")
            {
                // Якщо натиснули кнопку "Скинути", очищуємо дані
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // Зчитуємо дані з форми
                string name = form["Name"];
                string phone = form["Phone"];
                string email = form["Email"];
                string birthdate = form["Birthdate"];

                // Записуємо дані у ViewBag для виведення
                ViewBag.Name = name;
                ViewBag.Phone = phone;
                ViewBag.Email = email;
                ViewBag.Birthdate = birthdate;
            }

            // Повертаємо View з оновленими даними
            return View();
        }
    }
}
