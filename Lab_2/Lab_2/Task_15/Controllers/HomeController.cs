using Microsoft.AspNetCore.Mvc;

namespace Task_15.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // �� ������������� ������������ "���" ��� ���������� �� ���������
            ViewBag.ComboCity = "���";
            ViewBag.ListCity = "���";
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        // ������ ��������: ��������� ����� ����������� �� �����
        [HttpPost]
        public IActionResult Index(string comboCity, string listCity, string action)
        {
            if (action == "reset")
            {
                // ���� ��������� "�������", ��������� �������� �� �������������
                ViewBag.ComboCity = "���";
                ViewBag.ListCity = "���";
                ViewBag.ShowResult = false;
            }
            else
            {
                // ������ ������� ������ �������� � �����
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
