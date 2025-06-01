using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_7.Data;
using Task_7.Models;

namespace Task_7.Controllers
{
    public class TransportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportController(ApplicationDbContext context)
        {
            _context = context; // Ініціалізація контексту БД
        }

        // GET: Transport - вивід списку маршрутів
        public async Task<IActionResult> Index()
        {
            var transports = await _context.Transports.ToListAsync();
            return View(transports);
        }

        // GET: Transport/Details/5 - перегляд деталей маршруту
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var transport = await _context.Transports.FirstOrDefaultAsync(m => m.Id == id);
            if (transport == null)
                return NotFound();

            return View(transport);
        }

        // GET: Transport/Create - форма для створення маршруту
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transport/Create - збереження нового маршруту
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VidTransportu,NomMarshruta,ProtjazhnistMarshruta,ChasVDorozi")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transport);
        }

        // GET: Transport/Edit/5 - форма редагування маршруту
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
                return NotFound();

            return View(transport);
        }

        // POST: Transport/Edit/5 - збереження змін маршруту
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VidTransportu,NomMarshruta,ProtjazhnistMarshruta,ChasVDorozi")] Transport transport)
        {
            if (id != transport.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transport);
        }

        // GET: Transport/Delete/5 - підтвердження видалення маршруту
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var transport = await _context.Transports.FirstOrDefaultAsync(m => m.Id == id);
            if (transport == null)
                return NotFound();

            return View(transport);
        }

        // POST: Transport/Delete/5 - видалення маршруту
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport != null)
            {
                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Перевірка наявності маршруту
        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.Id == id);
        }
    }
}
