using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
