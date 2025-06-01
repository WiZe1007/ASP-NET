using Microsoft.AspNetCore.Mvc;
using Task_2_2.Data;
using Task_2_2.Models;

namespace Task_2_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Головне подання з формою
            return View();
        }

        // POST: /Home/IndexPost
        [HttpPost]
        public IActionResult IndexPost()
        {
            // Зчитуємо URL трьох зображень із форми
            string image1Url = Request.Form["image1Url"];
            string image2Url = Request.Form["image2Url"];
            string image3Url = Request.Form["image3Url"];

            // Беремо з БД 3 записів (за Id = 1,2,3)
            var dim1 = _context.ImageDimensions.Find(1);
            var dim2 = _context.ImageDimensions.Find(2);
            var dim3 = _context.ImageDimensions.Find(3);

            // Передаємо в ViewBag
            ViewBag.Image1Url = image1Url;
            ViewBag.Image2Url = image2Url;
            ViewBag.Image3Url = image3Url;

            ViewBag.Image1Width = dim1?.Width ?? "60px";
            ViewBag.Image1Height = dim1?.Height ?? "120px";

            ViewBag.Image2Width = dim2?.Width ?? "300px";
            ViewBag.Image2Height = dim2?.Height ?? "100px";

            ViewBag.Image3Width = dim3?.Width ?? "150px";
            ViewBag.Image3Height = dim3?.Height ?? "50px";

            return View("Display");
        }

        public IActionResult Display()
        {
            return View();
        }
    }
}
