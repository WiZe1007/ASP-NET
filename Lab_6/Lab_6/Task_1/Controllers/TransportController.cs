using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class TransportController : Controller
    {
        // GET: /Transport/Index - повертає порожню форму
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Повернення форми без даних
        }

        // POST: /Transport/Index - обробка даних форми
        [HttpPost]
        public IActionResult Index(string transportType, string routeNumber, double routeDistance, int travelTime)
        {
            // Створення об'єкта Transport з переданими даними
            Transport transport = new Transport
            {
                Id = 1, // Демонстраційне значення
                TransportType = transportType,
                RouteNumber = routeNumber,
                RouteDistance = routeDistance,
                TravelTime = travelTime
            };

            return View(transport); // Відображення даних користувача
        }
    }
}
