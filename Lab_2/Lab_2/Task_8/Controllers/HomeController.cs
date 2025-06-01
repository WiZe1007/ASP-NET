using Microsoft.AspNetCore.Mvc;
using Task_8.Models;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ѕовертаЇмо модель ≥з дефолтними значенн€ми
            var model = new SelectionModel();
            ViewBag.ShowResult = false; // –езультат спочатку не показуЇтьс€
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SelectionModel model, string action)
        {
            if (action == "cancel")
            {
                // якщо натиснуто "¬≥дмовитис€" Ц очищуЇмо ModelState ≥ повертаЇмо нову модель
                ModelState.Clear();
                model = new SelectionModel();
                ViewBag.ShowResult = false;
            }
            else
            {
                // ≤накше Ц показуЇмо результат вибору
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
