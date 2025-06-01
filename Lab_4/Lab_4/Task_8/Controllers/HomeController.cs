using Microsoft.AspNetCore.Mvc;
using Task_8.Data;
using Task_8.Models;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        // DbContext ��� ������� �� ���� �����
        private readonly AppDbContext _db;

        // �����������, ���� ������ DbContext ����� dependency injection
        public HomeController(AppDbContext db)
        {
            _db = db; // �������� �������� ��� ������������ � ������� ����������
        }

        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // ����������� ��� ����������� � ���� �����
            var employees = _db.Employees.ToList();

            return View(employees);
        }

        // POST: /Home/Index (���������� ���������� � ������)
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            // ����� ����������� ��� ����������� (���������� ��� �������� � View)
            var employees = _db.Employees.ToList();

            return View(employees);
        }
    }
}
