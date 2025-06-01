using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class Page2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
