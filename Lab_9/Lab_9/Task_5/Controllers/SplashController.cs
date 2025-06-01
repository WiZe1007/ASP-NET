using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
