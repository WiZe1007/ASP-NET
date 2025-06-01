using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
