using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - �������� ������� �����
        [HttpGet]
        public IActionResult Index()
        {
            // ��������� ������ ��� ����������� �����
            return View();
        }

        // POST: /Home/Index - �������� ��� �����
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            // �������� �������� � ����� ����� IFormCollection
            string name = form["Name"];
            string phone = form["Phone"];
            string email = form["Email"];
            string visitDate = form["VisitDate"];

            string age = form["Age"];
            string cuisine = form["Cuisine"];
            string dishes = form["Dishes"];

            string reason = form["Reason"];
            string recommend = form["Recommend"];

            // �������� ��� � View ����� ViewBag ��� �����������
            ViewBag.Name = name;
            ViewBag.Phone = phone;
            ViewBag.Email = email;
            ViewBag.VisitDate = visitDate;
            ViewBag.Age = age;
            ViewBag.Cuisine = cuisine;
            ViewBag.Dishes = dishes;
            ViewBag.Reason = reason;
            ViewBag.Recommend = recommend;

            // ��������� ��� ����� ������ � ������, �� ���� ������
            return View();
        }
    }
}
