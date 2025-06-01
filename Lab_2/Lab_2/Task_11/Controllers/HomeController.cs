using Microsoft.AspNetCore.Mvc;

namespace Task_11.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // ������������ �������� ��������: ����� �� ������������ �������
            ViewBag.Sport = true;
            ViewBag.Travel = false;
            ViewBag.Craft = true;
            ViewBag.Draw = false;

            ViewBag.ShowResult = false; // �� ����� �� �������� ���������
            return View();
        }

        [HttpPost]
        public IActionResult Index(bool Sport, bool Travel, bool Craft, bool Draw, string action)
        {
            if (action == "cancel")
            {
                // ���� ���������� �������� "³���������", ��������� �������� ������������
                ViewBag.Sport = true;
                ViewBag.Travel = false;
                ViewBag.Craft = true;
                ViewBag.Draw = false;
                ViewBag.ShowResult = false;
            }
            else
            {
                // ���� ��������� "�������", �������� ��� � �����
                ViewBag.Sport = Sport;
                ViewBag.Travel = Travel;
                ViewBag.Craft = Craft;
                ViewBag.Draw = Draw;
                ViewBag.ShowResult = true;
            }

            return View();
        }
    }
}
