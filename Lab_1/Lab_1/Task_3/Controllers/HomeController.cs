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
            // ������� ������� � ������ � �� ������ ������� �����
            return View();
        }

        // POST: Home/IndexPost (��� �������� �������� � GET Index)
        [HttpPost]
        public IActionResult IndexPost()
        {
            // 1. ������� ������� ����� �� �����
            string rowStr = Request.Form["rowCount"];
            int rowCount = 0;
            int.TryParse(rowStr, out rowCount);

            // 2. ���������� �� ��, ��� �������� ������� ���������
            //    ����������, �� ������ ������ ����� �� Id=1
            ColumnCount colData = _context.ColumnCounts.Find(1);
            int colCount = colData?.Columns ?? 5; // ���� null, ������� 5 �� �������������

            // 3. ���������� HTML-��� ������� (������)
            //    ��� Razor-�����, ���� ������������� C#-���� � StringBuilder
            StringBuilder sb = new StringBuilder();

            sb.Append("<table style='border-collapse: collapse;'>");

            // ����� ��� ������ ������
            int cellNumber = 1;

            for (int r = 0; r < rowCount; r++)
            {
                sb.Append("<tr>");

                for (int c = 0; c < colCount; c++)
                {
                    // ��������� ���� ��� (�������) � ���� (r+c) �����, ��� ������, ������ ���
                    string bgColor = ((r + c) % 2 == 0) ? "#ffffff" : "#cccccc";

                    // HTML ������
                    sb.Append($"<td style='width:50px; height:50px; border:1px solid #000; background-color:{bgColor}; text-align:center;'>");
                    sb.Append(cellNumber);
                    sb.Append("</td>");

                    cellNumber++;
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            // �������� ������������ HTML � ViewBag
            ViewBag.TableHtml = sb.ToString();

            // ʳ������ �����
            ViewBag.Rows = rowCount;
            // ʳ������ ��������
            ViewBag.Cols = colCount;

            return View("Display");
        }

        // Display, �� �������� ����������� �������
        public IActionResult Display()
        {
            return View();
        }
    }
}
