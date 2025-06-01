using Microsoft.AspNetCore.Mvc;
using Task_6.Models;
using System.Linq;

namespace Task_6.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _context;

        public TransportController(TransportContext context)
        {
            _context = context; // Ініціалізація доступу до БД
        }

        // GET: /Transport/Index – відображаємо форму та записи
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Transport(); // Створюємо порожню модель
            var transports = _context.Transports.ToList(); // Отримуємо записи з БД
            ViewBag.Transports = transports;
            return View(model);
        }

        // POST: /Transport/Index – додаємо новий запис
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            _context.Transports.Add(transport); // Додавання запису
            _context.SaveChanges(); // Збереження змін

            var transports = _context.Transports.ToList(); // Оновлюємо список записів
            ViewBag.Transports = transports;
            return View(transport);
        }

        // POST: /Transport/DeleteAll – видаляємо всі записи
        [HttpPost]
        public IActionResult DeleteAll()
        {
            _context.Transports.RemoveRange(_context.Transports); // Видалення всіх записів
            _context.SaveChanges();

            var model = new Transport(); // Порожня модель для форми
            ViewBag.Transports = _context.Transports.ToList(); // Оновлений список (має бути порожнім)
            return View("Index", model);
        }

        // POST: /Transport/DeleteRecord – видаляємо конкретний запис
        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var record = _context.Transports.FirstOrDefault(t => t.Id == id);
            if (record != null)
            {
                _context.Transports.Remove(record); // Видаляємо знайдений запис
                _context.SaveChanges();
            }
            var model = new Transport();
            ViewBag.Transports = _context.Transports.ToList();
            return View("Index", model);
        }

        // GET: /Transport/Edit/{id} – відображення форми редагування
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var record = _context.Transports.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return RedirectToAction("Index"); // Якщо запис не знайдено
            }
            return View(record);
        }

        // POST: /Transport/Edit – збереження відредагованих даних
        [HttpPost]
        public IActionResult Edit(Transport transport)
        {
            if (ModelState.IsValid)
            {
                _context.Transports.Update(transport); // Оновлення даних
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transport);
        }
    }
}
