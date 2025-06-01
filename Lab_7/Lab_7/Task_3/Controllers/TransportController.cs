using Microsoft.AspNetCore.Mvc;
using Task_3.Models;

namespace Task_3.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _db;
        public TransportController(TransportContext db) => _db = db;

        // ---------- INDEX (GET) ----------
        public IActionResult Index()
        {
            ViewBag.Transports = _db.Transports.ToList();
            return View(new Transport());
        }

        // ---------- INDEX (POST) ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Transport transport)
        {
            if (!ModelState.IsValid)               // DataAnnotations спрацювали
            {
                ViewBag.Transports = _db.Transports.ToList();
                return View(transport);
            }

            _db.Add(transport);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); // PRG‑патерн
        }

        // ---------- DELETE ALL ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAll()
        {
            _db.Transports.RemoveRange(_db.Transports);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ---------- DELETE SINGLE ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // ---------- EDIT (GET) ----------
        public IActionResult Edit(int id)
        {
            var rec = _db.Transports.Find(id);
            return rec is null ? RedirectToAction(nameof(Index)) : View(rec);
        }

        // ---------- EDIT (POST) ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transport transport)
        {
            if (!ModelState.IsValid) return View(transport);

            _db.Update(transport);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
