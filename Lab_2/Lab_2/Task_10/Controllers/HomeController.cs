using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // ��� ������ � IFormCollection
using System.Collections.Generic;
using System.Linq;

namespace Task_10.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // ������ �������� ��������: "�����" �� "������������"
            ViewBag.SelectedInterests = new List<string> { "�����", "������������" };
            // �������� ��������� �� ����������
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(IFormCollection form, string action)
        {
            if (action == "cancel")
            {
                // ���� ���������� �������� "³���������", ��������� ������� ��������
                ViewBag.SelectedInterests = new List<string> { "�����", "������������" };
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "�������", ������ �������� �������� �� �����
                var selected = form["Interests"]; // �������� ������ ��������
                List<string> selectedList = selected.ToList(); // ������������ �� � ������

                ViewBag.SelectedInterests = selectedList;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
