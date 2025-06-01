using Microsoft.AspNetCore.Mvc;
using Task_5.Models;
using System.Linq;

namespace Task_5.Controllers
{
    public class TransportController : Controller
    {
        private readonly TransportContext _context;

        public TransportController(TransportContext context)
        {
            _context = context;
        }

        // GET: /Transport/Index
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Transport();
            var transports = _context.Transports.ToList();
            ViewBag.Transports = transports;
            return View(model);
        }

        // POST: /Transport/Index – додавання запису в БД
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            _context.Transports.Add(transport);
            _context.SaveChanges();

            var transports = _context.Transports.ToList();
            ViewBag.Transports = transports;
            return View(transport);
        }

        // POST: /Transport/DeleteAll – видалення всіх записів з таблиці
        [HttpPost]
        public IActionResult DeleteAll()
        {
            _context.Transports.RemoveRange(_context.Transports);
            _context.SaveChanges();

            var model = new Transport();
            ViewBag.Transports = _context.Transports.ToList();
            return View("Index", model);
        }

        // POST: /Transport/DeleteRecord – видалення окремого запису з таблиці
        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var record = _context.Transports.FirstOrDefault(t => t.Id == id);
            if (record != null)
            {
                _context.Transports.Remove(record);
                _context.SaveChanges();
            }
            var model = new Transport();
            ViewBag.Transports = _context.Transports.ToList();
            return View("Index", model);
        }
    }
}
