using Microsoft.AspNetCore.Mvc;

namespace Task_6.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
