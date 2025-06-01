using Microsoft.AspNetCore.Mvc;
using Task_16.Models;

namespace Task_16.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ������ � ���������� ���������� ("���" ��� ����)
            var model = new CitySelectionModel();
            ViewBag.ShowResult = false;
            return View(model);
        }

        // POST: /Home/Index
        // ������ �������� � ��� ����� ����'���� �� ������������ �����
        [HttpPost]
        public IActionResult Index(CitySelectionModel model, string action)
        {
            if (action == "reset")
            {
                // ���� ��������� "�������" � ��������� �������� ������ � ������� ModelState
                model = new CitySelectionModel();
                ViewBag.ShowResult = false;
                ModelState.Clear();  // ������� ModelState, ��� �� ���� ��������� �������
            }
            else
            {
                // ���� ��������� "³������", �������� ������ � �����
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
