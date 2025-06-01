using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Controller2 : Controller
    {
        // GET: Controller2/View2
        [HttpGet]
        public IActionResult View2()
        {
            // Перевірка: якщо сторона A не введена, повертаємося на першу сторінку
            if (TempData["SideA"] == null)
            {
                return RedirectToAction("View1", "Controller1");
            }
            // Зберігаємо значення сторони A для наступних запитів
            TempData.Keep("SideA");
            return View();
        }

        // POST: Controller2/View2
        [HttpPost]
        public IActionResult View2(string sideB)
        {
            // Записуємо значення сторони B у сесію
            HttpContext.Session.SetString("SideB", sideB);
            // Перехід до наступного введення (сторона C)
            return RedirectToAction("View3", "Controller3");
        }
    }
}
