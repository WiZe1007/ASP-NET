using Microsoft.AspNetCore.Mvc; 
using Task_7.Models; 

namespace Task_7.Controllers
{
    public class HomeController : Controller
    {
        // ћетод обробл€Ї €к GET, так ≥ POST запити
        [HttpGet, HttpPost]
        public IActionResult Index(StringProcessingViewModel? model)
        {
            // якщо модель пуста, створюЇмо новий екземпл€р
            if (model == null)
            {
                model = new StringProcessingViewModel();
            }

            // якщо запит POST ≥ натиснута кнопка "—кинути"
            if (Request.Method == "POST" && Request.Form["action"] == "Reset")
            {
                // ќчищаЇмо ModelState, щоб скинути вс≥ дан≥
                ModelState.Clear();

                // ѕовертаЇмо нову порожню модель
                model = new StringProcessingViewModel();
                return View(model);
            }

            // ¬≥дправл€Їмо модель у View
            return View(model);
        }
    }
}
