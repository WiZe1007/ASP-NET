using Microsoft.AspNetCore.Mvc;
using Task_5.Models;
using System;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - повертаємо порожню форму
        [HttpGet]
        public IActionResult Index()
        {
            // Створюємо нову модель для трикутника
            var model = new TriangleViewModel();
            return View(model);
        }

        // POST: /Home/Index - обробляємо дані з форми
        [HttpPost]
        public IActionResult Index(TriangleViewModel model)
        {
            // Якщо модель не пройшла валідацію, повертаємо форму з помилками
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Перевірка: значення гіпотенузи має бути більше нуля
            if (model.Hypotenuse <= 0)
            {
                model.ErrorMessage = "Значення гіпотенузи повинно бути більше нуля.";
                return View(model);
            }

            // Перевірка: обов'язково обрати тип розрахунку
            if (string.IsNullOrEmpty(model.CalculationType))
            {
                model.ErrorMessage = "Будь ласка, оберіть тип розрахунку.";
                return View(model);
            }

            // Розрахунок для рівнобічного прямокутного трикутника:
            // Периметр = h + 2*(h/√2) = h + h√2 = h*(1 + √2)
            // Площа = (h²)/4
            double h = model.Hypotenuse.Value;
            switch (model.CalculationType)
            {
                case "Периметр":
                    model.Perimeter = h * (1 + Math.Sqrt(2));
                    model.Area = null;
                    break;
                case "Площа":
                    model.Area = (h * h) / 4.0;
                    model.Perimeter = null;
                    break;
                case "Обидва":
                    model.Perimeter = h * (1 + Math.Sqrt(2));
                    model.Area = (h * h) / 4.0;
                    break;
                default:
                    model.ErrorMessage = "Невідомий тип розрахунку.";
                    break;
            }

            return View(model);
        }
    }
}
