using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Task_3.Controllers
{
    public class TriangleController : Controller
    {
        // Єдиний метод, який обробляє і GET, і POST запити на одну дію
        public IActionResult Index(string sideInput, bool reset = false)
        {
            // Якщо користувач натиснув "Нове обчислення" – чистимо збережені значення
            if (reset)
            {
                HttpContext.Session.Remove("SideA");
                HttpContext.Session.Remove("SideB");
                HttpContext.Session.Remove("SideC");
            }

            // Якщо запит був методом POST, обробляємо введене значення
            if (HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var sideA = HttpContext.Session.GetString("SideA");
                var sideB = HttpContext.Session.GetString("SideB");

                if (string.IsNullOrEmpty(sideA))
                {
                    // Перший ввід – записуємо як SideA
                    HttpContext.Session.SetString("SideA", sideInput);
                }
                else if (string.IsNullOrEmpty(sideB))
                {
                    // Другий ввід – записуємо як SideB
                    HttpContext.Session.SetString("SideB", sideInput);
                }
                else if (string.IsNullOrEmpty(HttpContext.Session.GetString("SideC")))
                {
                    // Третій ввід – записуємо як SideC
                    HttpContext.Session.SetString("SideC", sideInput);
                }
                // Редірект, щоб уникнути повторного сабміту форми
                return RedirectToAction("Index");
            }

            // Читаємо збережені значення сторін із сесії
            var savedSideA = HttpContext.Session.GetString("SideA");
            var savedSideB = HttpContext.Session.GetString("SideB");
            var savedSideC = HttpContext.Session.GetString("SideC");

            // Визначаємо поточний крок вводу залежно від того, які значення вже збережені
            int step = 1;
            if (string.IsNullOrEmpty(savedSideA))
                step = 1;
            else if (string.IsNullOrEmpty(savedSideB))
                step = 2;
            else if (string.IsNullOrEmpty(savedSideC))
                step = 3;
            else
                step = 4; // Усі сторони введено – переходимо до розрахунку

            ViewBag.Step = step;

            // Призначаємо CSS-клас для body, щоб змінити фон відповідно до кроку
            switch (step)
            {
                case 1: ViewBag.BodyClass = "bg1"; break;
                case 2: ViewBag.BodyClass = "bg2"; break;
                case 3: ViewBag.BodyClass = "bg3"; break;
                case 4: ViewBag.BodyClass = "bg4"; break;
            }

            // Якщо всі три сторони введено, виконуємо обчислення трикутника
            if (step == 4)
            {
                if (!double.TryParse(savedSideA, out double a))
                    a = 0;
                if (!double.TryParse(savedSideB, out double b))
                    b = 0;
                if (!double.TryParse(savedSideC, out double c))
                    c = 0;

                // Перевірка на коректність даних – чи можуть вони утворити трикутник
                if (a <= 0 || b <= 0 || c <= 0 || (a + b <= c) || (a + c <= b) || (b + c <= a))
                {
                    ViewBag.Result = "Введені значення не утворюють трикутник.";
                }
                else
                {
                    // Обчислюємо, чи має трикутник тупий кут
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
    }
}
