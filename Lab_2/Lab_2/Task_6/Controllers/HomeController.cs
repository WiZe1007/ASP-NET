using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Task_6.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ������������ �������� �������� ��� ����� ��� ������� �����������
            ViewBag.City = "Kharkiv";
            ViewBag.Transport = "Train";
            ViewBag.ShowResult = false;
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form, string action)
        {
            if (action == "cancel")
            {
                // ���� ��������� "³���������", ��������� ������� ��������
                ViewBag.City = "Kharkiv";
                ViewBag.Transport = "Train";
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "�������", ������� �������� � �����
                string city = form["City"];
                string transport = form["Transport"];
                ViewBag.City = city;
                ViewBag.Transport = transport;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
