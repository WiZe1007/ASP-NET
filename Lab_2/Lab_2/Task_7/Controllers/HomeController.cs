using Microsoft.AspNetCore.Mvc;

namespace Task_7.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ��� ������� ����������� ������������ ������� ��������
            ViewBag.City = "Kharkiv";
            ViewBag.Transport = "Train";
            ViewBag.ShowResult = false;
            return View();
        }

        // ��������� City, Transport �� action ����������� ������������� � �����
        [HttpPost]
        public IActionResult Index(string City, string Transport, string action)
        {
            if (action == "cancel")
            {
                // ���� ��������� "³���������", ��������� ��������� ����
                ViewBag.City = "Kharkiv";
                ViewBag.Transport = "Train";
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "�������", �������� �������� � ����� ��� �����������
                ViewBag.City = City;
                ViewBag.Transport = Transport;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
