using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - �������� ����� ��� �������� �����
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Home/Index - �������� ��� � �����
        [HttpPost]
        public IActionResult Index(string Name, string Phone, string Email, string VisitDate,
                                   string Age, string Cuisine, string Dishes,
                                   string Reason, string Recommend)
        {
            // �������� ������� ��� � ViewBag ��� ����������� � ������ �������
            ViewBag.Name = Name;
            ViewBag.Phone = Phone;
            ViewBag.Email = Email;
            ViewBag.VisitDate = VisitDate;
            ViewBag.Age = Age;
            ViewBag.Cuisine = Cuisine;
            ViewBag.Dishes = Dishes;
            ViewBag.Reason = Reason;
            ViewBag.Recommend = Recommend;

            // ��������� ��� ����� View, ��� �������� ���
            return View();
        }
    }
}
