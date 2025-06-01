using Microsoft.AspNetCore.Mvc;

namespace Task_2.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
