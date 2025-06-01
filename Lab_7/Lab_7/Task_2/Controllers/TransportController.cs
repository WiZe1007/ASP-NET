using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class TransportController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TransportController(ApplicationDbContext db) => _db = db;

        // ========== INDEX ==========
        public async Task<IActionResult> Index()
            => View(await _db.Transports.AsNoTracking().ToListAsync());

        // ========== DETAILS ==========
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        // ========== CREATE (GET) ==========
        public IActionResult Create() => View();

        // ========== CREATE (POST) ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transport transport)
        {
            Validate(transport);

            if (!ModelState.IsValid) return View(transport);

            _db.Add(transport);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ========== EDIT (GET) ==========
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        // ========== EDIT (POST) ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transport transport)
        {
            if (id != transport.Id) return NotFound();

            Validate(transport);

            if (!ModelState.IsValid) return View(transport);

            try
            {
                _db.Update(transport);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(transport.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // ========== DELETE (GET) ==========
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var t = await _db.Transports.FindAsync(id);
            return t is null ? NotFound() : View(t);
        }

        // ========== DELETE (POST) ==========
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

        // ---------- Перевірка наявності ----------
        private bool TransportExists(int id)
            => _db.Transports.Any(e => e.Id == id);

        // ---------- Власна валідація ----------
        private void Validate(Transport t)
        {
            // 1. Вид транспорту
            if (string.IsNullOrWhiteSpace(t.VidTransportu))
                ModelState.AddModelError(nameof(t.VidTransportu),
                    "Вид транспорту обов’язковий (Tr, Tl, A).");
            else if (!new[] { "Tr", "Tl", "A" }.Contains(t.VidTransportu))
                ModelState.AddModelError(nameof(t.VidTransportu),
                    "Допустимі лише Tr, Tl або A.");

            // 2. Номер маршруту
            if (string.IsNullOrWhiteSpace(t.NomMarshruta))
                ModelState.AddModelError(nameof(t.NomMarshruta),
                    "Номер маршруту обов’язковий.");
            else if (t.NomMarshruta.Length > 15)
                ModelState.AddModelError(nameof(t.NomMarshruta),
                    "Максимум 15 символів.");

            // 3. Протяжність
            if (t.ProtjazhnistMarshruta <= 0)
                ModelState.AddModelError(nameof(t.ProtjazhnistMarshruta),
                    "Протяжність має бути > 0 км.");

            // 4. Час у дорозі
            if (t.ChasVDorozi < 1)
                ModelState.AddModelError(nameof(t.ChasVDorozi),
                    "Час має бути ≥ 1 хв.");

            // 5. Кількість зупинок
            if (t.KilkistZupynok < 0)
                ModelState.AddModelError(nameof(t.KilkistZupynok),
                    "Кількість зупинок не може бути від’ємною.");
        }
    }
}
