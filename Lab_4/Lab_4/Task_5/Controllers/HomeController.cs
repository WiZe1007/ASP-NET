using Microsoft.AspNetCore.Mvc;
using Task_5.Models;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - ���������� ������� ����� ��� ����������
        [HttpGet]
        public IActionResult Index()
        {
            return View(new VisitorViewModel());
        }

        // POST: /Home/Index - ���������� ���� � ����� � ��������� �� � ������ �����
        [HttpPost]
        public IActionResult Index(VisitorViewModel model)
        {
            // ����� ������ �������� ModelState.IsValid, ���� �������
            return View(model);
        }
    }
}
