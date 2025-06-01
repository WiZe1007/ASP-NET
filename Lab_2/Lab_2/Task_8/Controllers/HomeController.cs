using Microsoft.AspNetCore.Mvc;
using Task_8.Models;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ������ �� ���������� ����������
            var model = new SelectionModel();
            ViewBag.ShowResult = false; // ��������� �������� �� ����������
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SelectionModel model, string action)
        {
            if (action == "cancel")
            {
                // ���� ��������� "³���������" � ������� ModelState � ��������� ���� ������
                ModelState.Clear();
                model = new SelectionModel();
                ViewBag.ShowResult = false;
            }
            else
            {
                // ������ � �������� ��������� ������
                ViewBag.ShowResult = true;
            }
            return View(model);
        }
    }
}
