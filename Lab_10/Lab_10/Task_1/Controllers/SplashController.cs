using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
