using Microsoft.AspNetCore.Mvc;

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
