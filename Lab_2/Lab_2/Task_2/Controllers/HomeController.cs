using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // ������� ��� IFormCollection

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        // ������� GET-������ ��� �������
        [HttpGet]
        public IActionResult Index()
        {
            // ������ ��������� View ��� �����
            return View();
        }

        // ������� POST-������ � �����
        [HttpPost]
        public IActionResult Index(IFormCollection form, string action)
        {
            if (action == "reset")
            {
                // ���� ��������� ������ "�������", ������� ���
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // ������� ��� � �����
                string name = form["Name"];
                string phone = form["Phone"];
                string email = form["Email"];
                string birthdate = form["Birthdate"];

                // �������� ��� � ViewBag ��� ���������
                ViewBag.Name = name;
                ViewBag.Phone = phone;
                ViewBag.Email = email;
                ViewBag.Birthdate = birthdate;
            }

            // ��������� View � ���������� ������
            return View();
        }
    }
}
