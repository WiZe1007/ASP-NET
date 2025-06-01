using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - �������� �����
        [HttpGet]
        public IActionResult Index()
        {
            // ������ ��������� ������������� (View)
            return View();
        }

        // POST: /Home/Index - ������� ����� �����
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            // �������� ��� � ����� ����� Request.Form
            string name = Request.Form["Name"];
            string phone = Request.Form["Phone"];
            string email = Request.Form["Email"];
            string visitDate = Request.Form["VisitDate"];

            string age = Request.Form["Age"];
            string cuisine = Request.Form["Cuisine"];
            string dishes = Request.Form["Dishes"];

            string reason = Request.Form["Reason"];
            string recommend = Request.Form["Recommend"];

            // �������� ��� � ViewBag ��� ������ � ������������
            ViewBag.Name = name;
            ViewBag.Phone = phone;
            ViewBag.Email = email;
            ViewBag.VisitDate = visitDate;
            ViewBag.Age = age;
            ViewBag.Cuisine = cuisine;
            ViewBag.Dishes = dishes;
            ViewBag.Reason = reason;
            ViewBag.Recommend = recommend;

            // ��������� �� ���� ������� � ���������� ������
            return View();
        }
    }
}
