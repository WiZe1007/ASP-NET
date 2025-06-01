using Microsoft.AspNetCore.Mvc;
using Task_8.Data;
using Task_8.Models;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        // DbContext для доступу до бази даних
        private readonly AppDbContext _db;

        // Конструктор, який отримує DbContext через dependency injection
        public HomeController(AppDbContext db)
        {
            _db = db; // зберігаємо контекст для використання в методах контролера
        }

        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Завантажуємо всіх співробітників з бази даних
            var employees = _db.Employees.ToList();

            return View(employees);
        }

        // POST: /Home/Index (фільтрація відбувається у поданні)
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            // Знову завантажуємо всіх співробітників (фільтрація вже робиться у View)
            var employees = _db.Employees.ToList();

            return View(employees);
        }
    }
}
