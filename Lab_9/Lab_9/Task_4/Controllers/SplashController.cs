using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
