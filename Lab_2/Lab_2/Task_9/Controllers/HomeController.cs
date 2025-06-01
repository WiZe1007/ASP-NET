using Microsoft.AspNetCore.Mvc;

namespace Task_9.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // ������ ������� ��������
            ViewBag.SelectedInterests = new List<string> { "�����", "������������" };
            // �������� �� �������� ���������
            ViewBag.ShowResult = false;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public IActionResult Index(string action)
        {
            if (action == "cancel")
            {
                // ���� ���������� �������� "³���������" - ��������� ������� ��������
                ViewBag.SelectedInterests = new List<string> { "�����", "������������" };
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "�������", ������� ������ ��������
                var selected = Request.Form["Interests"]; // �������� ��� � �����
                List<string> selectedList = selected.ToList(); // ������������ � ������

                ViewBag.SelectedInterests = selectedList;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
