using Microsoft.AspNetCore.Mvc;

namespace Task_13.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // �� ������������� �������� "���" ��� ���������� �� ���������
            ViewBag.ComboCity = "���";
            ViewBag.ListCity = "���";
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            // ������� �������� ������ � ����� ����� Request
            string action = Request.Form["action"];
            if (action == "reset")
            {
                // ���� ��������� "�������" � ��������� ������� ��������
                ViewBag.ComboCity = "���";
                ViewBag.ListCity = "���";
                ViewBag.ShowResult = false;
            }
            else
            {
                // ������ ������� ����� �������� � ���������� �� ��������� ����� Request
                string comboCity = Request.Form["comboCity"];
                string listCity = Request.Form["listCity"];
                ViewBag.ComboCity = comboCity;
                ViewBag.ListCity = listCity;
                ViewBag.ShowResult = true;
            }
            return View();
        }
    }
}
