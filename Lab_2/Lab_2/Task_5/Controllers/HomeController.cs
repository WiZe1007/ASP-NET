using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // ������������ ������� �������� ��� ����-������
            ViewBag.City = "Kharkiv";       // �� ������������� � �����
            ViewBag.Transport = "Train";     // �� ������������� � �����
            ViewBag.ShowResult = false;      // ������ ������ � ��������� �� ����������
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(string action)
        {
            if (action == "cancel")
            {
                // ���� ��������� "³���������", ��������� ��������� ����
                ViewBag.City = "Kharkiv";
                ViewBag.Transport = "Train";
                ViewBag.ShowResult = false; // ��������� �� ����������
            }
            else
            {
                // ���� ��������� "�������", ������� ���� ����������� � �����
                string city = Request.Form["City"];
                string transport = Request.Form["Transport"];

                // �������� ������� �������� ��� ���������� �����������
                ViewBag.City = city;
                ViewBag.Transport = transport;
                ViewBag.ShowResult = true;  // �������� ���������
            }

            return View();
        }
    }
}
