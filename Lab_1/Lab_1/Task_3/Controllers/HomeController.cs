using Microsoft.AspNetCore.Mvc;
using Task_3.Data;
using Task_3.Models;
using System.Text;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Головна сторінка з формою — де задаємо кількість рядків
            return View();
        }

        // POST: Home/IndexPost (щоб уникнути конфлікту з GET Index)
        [HttpPost]
        public IActionResult IndexPost()
        {
            // 1. Зчитуємо кількість рядків із форми
            string rowStr = Request.Form["rowCount"];
            int rowCount = 0;
            int.TryParse(rowStr, out rowCount);

            // 2. Звертаємося до БД, щоб отримати кількість стовпчиків
            //    Припустимо, що завжди беремо запис із Id=1
            ColumnCount colData = _context.ColumnCounts.Find(1);
            int colCount = colData?.Columns ?? 5; // якщо null, візьмемо 5 за замовчуванням

            // 3. Побудувати HTML-код таблиці (шахова)
            //    Без Razor-циклів, отже використовуємо C#-цикл і StringBuilder
            StringBuilder sb = new StringBuilder();

            sb.Append("<table style='border-collapse: collapse;'>");

            // Змінна для номера комірки
            int cellNumber = 1;

            for (int r = 0; r < rowCount; r++)
            {
                sb.Append("<tr>");

                for (int c = 0; c < colCount; c++)
                {
                    // Визначаємо колір тла (шаховий) — якщо (r+c) парне, біла комірка, інакше сіра
                    string bgColor = ((r + c) % 2 == 0) ? "#ffffff" : "#cccccc";

                    // HTML комірки
                    sb.Append($"<td style='width:50px; height:50px; border:1px solid #000; background-color:{bgColor}; text-align:center;'>");
                    sb.Append(cellNumber);
                    sb.Append("</td>");

                    cellNumber++;
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            // Передаємо згенерований HTML у ViewBag
            ViewBag.TableHtml = sb.ToString();

            // Кількість рядків
            ViewBag.Rows = rowCount;
            // Кількість стовпців
            ViewBag.Cols = colCount;

            return View("Display");
        }

        // Display, де показуємо згенеровану таблицю
        public IActionResult Display()
        {
            return View();
        }
    }
}
