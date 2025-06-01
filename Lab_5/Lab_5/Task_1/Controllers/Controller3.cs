using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Controller3 : Controller
    {
        // GET: Controller3/View3
        [HttpGet]
        public IActionResult View3()
        {
            // Переконуємось, що дані сторін A та B вже введені
            if (TempData["SideA"] == null || HttpContext.Session.GetString("SideB") == null)
            {
                return RedirectToAction("View1", "Controller1");
            }
            // Залишаємо дані сторони A для подальшого використання
            TempData.Keep("SideA");
            return View();
        }

        // POST: Controller3/View3
        [HttpPost]
        public IActionResult View3(string sideC)
        {
            // Передаємо значення сторони C через параметри запиту до наступного контролера
            return RedirectToAction("View4", "Controller4", new { sideC = sideC });
        }
    }
}
