using Microsoft.AspNetCore.Http; // ��� ������ � IFormCollection
using Microsoft.AspNetCore.Mvc;

namespace Task_14.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // �� ������������� �������� "���" ��� ���������� �� ���������
            ViewBag.ComboCity = "���";
            ViewBag.ListCity = "���";
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            // ������� ��, ��������� � ���� (choose ��� reset)
            string action = form["action"];
            if (action == "reset")
            {
                // ���� ��������� "�������", ��������� ������� ��������
                ViewBag.ComboCity = "���";
                ViewBag.ListCity = "���";
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "³������", ������ �������� � ���������� �� ���������
                string comboCity = form["comboCity"];
                string listCity = form["listCity"];
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
