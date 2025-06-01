using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
{
    public class Page4Controller : Controller
    {
        public IActionResult Index()
        {
            const string sessionKey = "Page4Count";
            int count = (HttpContext.Session.GetInt32(sessionKey) ?? 0) + 1;
            HttpContext.Session.SetInt32(sessionKey, count);
            ViewBag.VisitCount = count;
            return View();
        }
    }
}
