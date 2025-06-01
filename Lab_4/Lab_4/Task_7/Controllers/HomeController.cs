using Microsoft.AspNetCore.Mvc; 
using Task_7.Models; 

namespace Task_7.Controllers
{
    public class HomeController : Controller
    {
        // ����� �������� �� GET, ��� � POST ������
        [HttpGet, HttpPost]
        public IActionResult Index(StringProcessingViewModel? model)
        {
            // ���� ������ �����, ��������� ����� ���������
            if (model == null)
            {
                model = new StringProcessingViewModel();
            }

            // ���� ����� POST � ��������� ������ "�������"
            if (Request.Method == "POST" && Request.Form["action"] == "Reset")
            {
                // ������� ModelState, ��� ������� �� ���
                ModelState.Clear();

                // ��������� ���� ������� ������
                model = new StringProcessingViewModel();
                return View(model);
            }

            // ³���������� ������ � View
            return View(model);
        }
    }
}
