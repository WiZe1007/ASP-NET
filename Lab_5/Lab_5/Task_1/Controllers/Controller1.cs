using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Controller1 : Controller
    {
        // GET: Controller1/View1
        [HttpGet]
        public IActionResult View1()
        {
            // Відображаємо сторінку для введення сторони A
            return View();
        }

        // POST: Controller1/View1
        [HttpPost]
        public IActionResult View1(string sideA)
        {
            // Зберігаємо значення сторони A для використання в наступних кроках
            TempData["SideA"] = sideA;
            // Перехід до введення сторони B у наступному контролері
            return RedirectToAction("View2", "Controller2");
        }
    }
}
