using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
