using Microsoft.AspNetCore.Mvc;
using Task_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Task_1.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _db;

        public TransportController(TransportContext db) => _db = db;

        // -------------------- GET --------------------
        public IActionResult Index()
        {
            ViewBag.Transports = _db.Transports.AsNoTracking().ToList();
            return View(new Transport());
        }

        // -------------------- POST: створення --------------------
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            Validate(transport);                 // ➊ власна перевірка

            if (!ModelState.IsValid)             // ➋ якщо є помилки – повертаємо форму
            {
                ViewBag.Transports = _db.Transports.AsNoTracking().ToList();
                return View(transport);
            }

            _db.Transports.Add(transport);       // ➌ усе ґуд – зберігаємо
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // -------------------- GET: редагування --------------------
        public IActionResult Edit(int id)
        {
            var t = _db.Transports.Find(id);
            return t is null ? RedirectToAction(nameof(Index)) : View(t);
        }

        // -------------------- POST: редагування --------------------
        [HttpPost]
        public IActionResult Edit(Transport transport)
        {
            Validate(transport);

            if (!ModelState.IsValid) return View(transport);

            _db.Transports.Update(transport);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // -------------------- POST: видалити все --------------------
        [HttpPost]
        public IActionResult DeleteAll()
        {
            _db.Transports.RemoveRange(_db.Transports);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // -------------------- POST: видалити один --------------------
        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var rec = _db.Transports.Find(id);
            if (rec != null)
            {
                _db.Transports.Remove(rec);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // -------------------- Приватна перевірка --------------------
        private void Validate(Transport t)
        {
            // Вид транспорту
            if (string.IsNullOrWhiteSpace(t.TransportType))
                ModelState.AddModelError(nameof(t.TransportType),
                    "Вид транспорту є обов’язковим!");
            else if (!new[] { "Tr", "Tl", "A" }.Contains(t.TransportType))
                ModelState.AddModelError(nameof(t.TransportType),
                    "Допустимі значення: Tr, Tl або A");

            // Номер маршруту
            if (string.IsNullOrWhiteSpace(t.RouteNumber))
                ModelState.AddModelError(nameof(t.RouteNumber),
                    "Номер маршруту обов’язковий");
            else if (t.RouteNumber.Length > 10)
                ModelState.AddModelError(nameof(t.RouteNumber),
                    "Максимум 10 символів");

            // Дистанція
            if (t.RouteDistance <= 0)
                ModelState.AddModelError(nameof(t.RouteDistance),
                    "Протяжність має бути > 0 км");

            // Час у дорозі
            if (t.TravelTime < 1)
                ModelState.AddModelError(nameof(t.TravelTime),
                    "Час має бути не менше 1 хв");
        }
    }
}
