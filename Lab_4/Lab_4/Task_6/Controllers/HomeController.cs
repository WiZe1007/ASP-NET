using Microsoft.AspNetCore.Mvc;
using Task_6.Models;

namespace Task_6.Controllers
{
    public class HomeController : Controller
    {
        // Один метод, що обробляє як GET, так і POST запити
        [HttpGet, HttpPost]
        public IActionResult Index(VisitorViewModel? model)
        {
            if (model == null)
            {
                model = new VisitorViewModel();
            }
            return View(model);
        }
    }
}
