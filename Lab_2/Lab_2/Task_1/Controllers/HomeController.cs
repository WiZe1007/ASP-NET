using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Повертаємо порожню форму при першому завантаженні сторінки
            return View();
        }

        [HttpPost]
        public IActionResult Index(string action)
        {
            // Визначаємо, яка кнопка була натиснута
            if (action == "reset")
            {
                // Якщо "Скинути", очищуємо дані
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // Зчитуємо дані з форми
                string name = Request.Form["Name"];
                string phone = Request.Form["Phone"];
                string email = Request.Form["Email"];
                string birthdate = Request.Form["Birthdate"];

                // Передаємо дані у ViewBag для виведення на сторінці
                ViewBag.Name = name;
                ViewBag.Phone = phone;
                ViewBag.Email = email;
                ViewBag.Birthdate = birthdate;
            }

            // Повертаємо сторінку з даними або очищеною формою
            return View();
        }
    }
}
