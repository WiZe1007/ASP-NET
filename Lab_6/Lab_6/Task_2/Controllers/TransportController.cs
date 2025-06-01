using Microsoft.AspNetCore.Mvc;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class TransportController : Controller
    {
        // GET: /Transport/Index - повертає порожню форму
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Transport/Index - приймає дані форми
        [HttpPost]
        public IActionResult Index(Transport transport)
        {
            // Присвоєння демонстраційного Id
            transport.Id = 1;
            return View(transport);
        }
    }
}
