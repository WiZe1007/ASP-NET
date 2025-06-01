using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - показуємо порожню форму
        [HttpGet]
        public IActionResult Index()
        {
            // Повертаємо вигляд для відображення форми
            return View();
        }

        // POST: /Home/Index - отримуємо дані форми
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            // Отримуємо значення з форми через IFormCollection
            string name = form["Name"];
            string phone = form["Phone"];
            string email = form["Email"];
            string visitDate = form["VisitDate"];

            string age = form["Age"];
            string cuisine = form["Cuisine"];
            string dishes = form["Dishes"];

            string reason = form["Reason"];
            string recommend = form["Recommend"];

            // Передаємо дані у View через ViewBag для відображення
            ViewBag.Name = name;
            ViewBag.Phone = phone;
            ViewBag.Email = email;
            ViewBag.VisitDate = visitDate;
            ViewBag.Age = age;
            ViewBag.Cuisine = cuisine;
            ViewBag.Dishes = dishes;
            ViewBag.Reason = reason;
            ViewBag.Recommend = recommend;

            // Повертаємо той самий вигляд з даними, що були введені
            return View();
        }
    }
}
