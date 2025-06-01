using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class TransportController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TransportController(ApplicationDbContext db) => _db = db;

        // ---------- INDEX ----------
        public async Task<IActionResult> Index()
            => View(await _db.Transports.AsNoTracking().ToListAsync());

        // ---------- DETAILS ----------
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        // ---------- CREATE ----------
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transport transport)
        {
            if (!ModelState.IsValid) return View(transport);

            _db.Add(transport);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---------- EDIT ----------
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transport transport)
        {
            if (id != transport.Id) return NotFound();
            if (!ModelState.IsValid) return View(transport);

            try
            {
                _db.Update(transport);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Transports.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // ---------- DELETE ----------
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _db.Transports.FindAsync(id);
            if (t != null)
            {
                _db.Transports.Remove(t);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
