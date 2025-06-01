using Microsoft.AspNetCore.Mvc;
using Task_12.Models;

namespace Task_12.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ���� ������ � ���������� ���������� (����� �� ������������ �������)
            var model = new InterestsModel();
            ViewBag.ShowResult = false; // �� ������� ��������� �� ����������
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(InterestsModel model, string action)
        {
            if (action == "cancel")
            {
                // ���� ���������� �������� "³���������" � ��������� ������ � ����������� ����������
                model = new InterestsModel();
                ViewBag.ShowResult = false;
                ModelState.Clear(); // ������� ModelState, ��� ������� �������� ���
            }
            else
            {
                // ���� ���� �����������, �������� ���������� � �����
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
