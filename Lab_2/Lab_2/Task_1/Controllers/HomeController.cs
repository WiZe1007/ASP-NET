using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ������� ����� ��� ������� ����������� �������
            return View();
        }

        [HttpPost]
        public IActionResult Index(string action)
        {
            // ���������, ��� ������ ���� ���������
            if (action == "reset")
            {
                // ���� "�������", ������� ���
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // ������� ��� � �����
                string name = Request.Form["Name"];
                string phone = Request.Form["Phone"];
                string email = Request.Form["Email"];
                string birthdate = Request.Form["Birthdate"];

                // �������� ��� � ViewBag ��� ��������� �� �������
                ViewBag.Name = name;
                ViewBag.Phone = phone;
                ViewBag.Email = email;
                ViewBag.Birthdate = birthdate;
            }

            // ��������� ������� � ������ ��� �������� ������
            return View();
        }
    }
}
