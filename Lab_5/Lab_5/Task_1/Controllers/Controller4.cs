using Microsoft.AspNetCore.Mvc;
using System;

namespace Task_1.Controllers
{
    public class Controller4 : Controller
    {
        // GET: Controller4/View4?sideC=...
        [HttpGet]
        public IActionResult View4(string sideC)
        {
            // Перевірка: чи є всі необхідні дані для обчислення
            if (TempData["SideA"] == null || HttpContext.Session.GetString("SideB") == null || string.IsNullOrEmpty(sideC))
            {
                return RedirectToAction("View1", "Controller1");
            }

            // Спроба перетворення введених даних у числа
            if (!double.TryParse(TempData["SideA"].ToString(), out double a))
            {
                a = 0; // В разі помилки встановлюємо значення 0
            }
            if (!double.TryParse(HttpContext.Session.GetString("SideB"), out double b))
            {
                b = 0;
            }
            if (!double.TryParse(sideC, out double c))
            {
                c = 0;
            }

            // Перевірка чи введені значення утворюють коректний трикутник
            if (a <= 0 || b <= 0 || c <= 0 || (a + b <= c) || (a + c <= b) || (b + c <= a))
            {
                ViewBag.Result = "Введені значення не утворюють трикутник.";
                return View();
            }

            // Обчислення: визначаємо чи є тупий кут у трикутнику
            // Знаходимо найдовшу сторону
            double longest = Math.Max(a, Math.Max(b, c));
            double sumSquares = 0;
            if (Math.Abs(longest - a) < 0.0001)
                sumSquares = b * b + c * c;
            else if (Math.Abs(longest - b) < 0.0001)
                sumSquares = a * a + c * c;
            else
                sumSquares = a * a + b * b;

            // Якщо квадрат найдовшої сторони більший за суму квадратів інших, трикутник має тупий кут
            bool isObtuse = (longest * longest > sumSquares);
            string resultText = isObtuse ? "трикутник має тупий кут." : "Тупий кут відстутній у трикутнику.";
            ViewBag.Result = $"Для трикутника зі сторонами: a = {a}, b = {b}, c = {c} => {resultText}";

            return View();
        }
    }
}
