using Microsoft.AspNetCore.Mvc;
using Task_3.Models;
using System.Linq;

namespace Task_3.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _context;

        public TransportController(TransportContext context)
        {
            _context = context; // Ініціалізація контексту бази даних
        }

        // GET: /Transport/Index - показує форму і таблицю записів
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Transport(); // Порожня модель для форми
            var transports = _context.Transports.ToList(); // Отримання записів з БД
            ViewBag.Transports = transports;
            return View(model);
        }

        // POST: /Transport/Index - додає новий запис до БД
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            _context.Transports.Add(transport); // Додавання запису
            _context.SaveChanges(); // Збереження змін

            var transports = _context.Transports.ToList(); // Оновлення списку записів
            ViewBag.Transports = transports;

            return View(transport);
        }
    }
}
