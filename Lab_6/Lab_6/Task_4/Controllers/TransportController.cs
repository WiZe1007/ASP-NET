using Microsoft.AspNetCore.Mvc;
using Task_4.Models;
using System.Linq;

namespace Task_4.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _context;

        public TransportController(TransportContext context)
        {
            _context = context; // Ініціалізація контексту даних
        }

        // GET: /Transport/Index
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Transport(); // Порожня модель для форми
            var transports = _context.Transports.ToList(); // Завантаження даних з БД
            ViewBag.Transports = transports;
            return View(model);
        }

        // POST: /Transport/Index - Додавання нового запису
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            _context.Transports.Add(transport); // Додавання запису до БД
            _context.SaveChanges(); // Збереження змін

            var transports = _context.Transports.ToList(); // Оновлення даних
            ViewBag.Transports = transports;

            return View(transport);
        }

        // POST: /Transport/DeleteAll - Видалення всіх записів
        [HttpPost]
        public IActionResult DeleteAll()
        {
            _context.Transports.RemoveRange(_context.Transports); // Видалення всіх записів
            _context.SaveChanges(); // Фіксація змін

            var model = new Transport(); // Порожня модель після видалення
            ViewBag.Transports = _context.Transports.ToList(); // Завантаження порожнього списку
            return View("Index", model);
        }
    }
}
