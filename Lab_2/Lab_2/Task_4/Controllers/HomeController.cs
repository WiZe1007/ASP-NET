using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        // GET-�����: ��������� ����� � ����� (���������) �������
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ����� ��'��� PersonModel �� ����������� ����������
            return View(new PersonModel());
        }

        // POST-�����: �������� ��� � ����� ����� ������ ��������� ������
        [HttpPost]
        public IActionResult Index(PersonModel model, string action)
        {
            if (action == "reset")
            {
                // ���� ��������� "�������", ������� ���� ����� �� ��������� ���� ������� ������
                ModelState.Clear(); // �������� ������� ��������, ���� ����
                model = new PersonModel();
            }
            // ��������� ������� � ��������� ��� �������� �������
            return View(model);
        }
    }
}
