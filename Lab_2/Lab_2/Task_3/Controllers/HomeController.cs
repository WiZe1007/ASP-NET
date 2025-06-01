using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        // GET-�����: ������ ��������� ����� ��� ��������� �����
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST-�����: �������� ��� � �����
        // ��������� Name, Phone, Email, Birthdate ���������� ������ ���� �����,
        // � �������� action �������, ��� ������ ���� ��������� (submit ��� reset)
        [HttpPost]
        public IActionResult Index(string Name, string Phone, string Email, string Birthdate, string action)
        {
            if (action == "reset")
            {
                // ���� ��������� "�������", ������� �� ����
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // ���� ��������� "³������", �������� ������� ��� ��� ���������� �����������
                ViewBag.Name = Name;
                ViewBag.Phone = Phone;
                ViewBag.Email = Email;
                ViewBag.Birthdate = Birthdate;
            }

            return View(); // ��������� ����� � ���������� ������
        }
    }
}
