using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - показуємо форму для введення даних
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Home/Index - отримуємо дані з форми
        [HttpPost]
        public IActionResult Index(string Name, string Phone, string Email, string VisitDate,
                                   string Age, string Cuisine, string Dishes,
                                   string Reason, string Recommend)
        {
            // Заносимо отримані дані у ViewBag для відображення у вигляді таблиці
            ViewBag.Name = Name;
            ViewBag.Phone = Phone;
            ViewBag.Email = Email;
            ViewBag.VisitDate = VisitDate;
            ViewBag.Age = Age;
            ViewBag.Cuisine = Cuisine;
            ViewBag.Dishes = Dishes;
            ViewBag.Reason = Reason;
            ViewBag.Recommend = Recommend;

            // Повертаємо той самий View, щоб показати дані
            return View();
        }
    }
}
