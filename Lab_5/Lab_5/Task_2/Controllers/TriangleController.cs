using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Task_2.Controllers
{
    public class TriangleController : Controller
    {
        // GET: Triangle/Index
        [HttpGet]
        public IActionResult Index(bool reset = false)
        {
            // Якщо користувач вирішив почати спочатку, очищаємо сесію
            if (reset)
            {
                HttpContext.Session.Remove("SideA");
                HttpContext.Session.Remove("SideB");
                HttpContext.Session.Remove("SideC");
            }

            // Отримуємо збережені дані введення сторін з сесії
            var sideA = HttpContext.Session.GetString("SideA");
            var sideB = HttpContext.Session.GetString("SideB");
            var sideC = HttpContext.Session.GetString("SideC");

            // Визначаємо, який крок зараз: якщо ще нічого не введено – перший крок,
            // якщо одна чи дві сторони – відповідний крок, інакше переходимо до розрахунку
            int step = 1;
            if (string.IsNullOrEmpty(sideA))
                step = 1;
            else if (string.IsNullOrEmpty(sideB))
                step = 2;
            else if (string.IsNullOrEmpty(sideC))
                step = 3;
            else
                step = 4; // Всі дані введено, можна проводити обчислення

            ViewBag.Step = step;

            // Змінюємо фон сторінки відповідно до поточного кроку
            switch (step)
            {
                case 1: ViewBag.BodyClass = "bg1"; break;
                case 2: ViewBag.BodyClass = "bg2"; break;
                case 3: ViewBag.BodyClass = "bg3"; break;
                case 4: ViewBag.BodyClass = "bg4"; break;
            }

            if (step == 4)
            {
                // Пробуємо перетворити введені рядки у числа
                if (!double.TryParse(sideA, out double a))
                    a = 0;
                if (!double.TryParse(sideB, out double b))
                    b = 0;
                if (!double.TryParse(sideC, out double c))
                    c = 0;

                // Перевірка на можливість утворення трикутника
                if (a <= 0 || b <= 0 || c <= 0 || (a + b <= c) || (a + c <= b) || (b + c <= a))
                {
                    ViewBag.Result = "Введені значення не утворюють трикутник.";
                }
                else
                {
                    // Розрахунок: перевіряємо чи має трикутник тупий кут
                    double longest = Math.Max(a, Math.Max(b, c));
                    double sumSquares = 0;
                    if (Math.Abs(longest - a) < 0.0001)
                        sumSquares = b * b + c * c;
                    else if (Math.Abs(longest - b) < 0.0001)
                        sumSquares = a * a + c * c;
                    else
                        sumSquares = a * a + b * b;

                    bool isObtuse = (longest * longest > sumSquares);
                    string resultText = isObtuse ? "трикутник має тупий кут." : "тупий кут відсутній у трикутнику.";
                    ViewBag.Result = $"Для трикутника зі сторонами: a = {a}, b = {b}, c = {c} => {resultText}";
                }
            }

            return View();
        }

        // POST: Triangle/Index
        [HttpPost]
        public IActionResult Index(string sideInput)
        {
            // Отримуємо поточні значення сторін із сесії
            var sideA = HttpContext.Session.GetString("SideA");
            var sideB = HttpContext.Session.GetString("SideB");

            if (string.IsNullOrEmpty(sideA))
            {
                // Якщо ще не введено першу сторону – записуємо в SideA
                HttpContext.Session.SetString("SideA", sideInput);
            }
            else if (string.IsNullOrEmpty(sideB))
            {
                // Якщо перша сторона вже введена, записуємо другу сторону
                HttpContext.Session.SetString("SideB", sideInput);
            }
            else
            {
                // Якщо введені перша і друга сторони – записуємо третю
                HttpContext.Session.SetString("SideC", sideInput);
            }

            // Перенаправляємо користувача назад на Index для оновлення кроку
            return RedirectToAction("Index");
        }
    }
}
