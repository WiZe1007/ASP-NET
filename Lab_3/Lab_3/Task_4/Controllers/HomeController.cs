using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index - відображаємо порожню форму для заповнення
        [HttpGet]
        public IActionResult Index()
        {
            // Створюємо новий екземпляр моделі і передаємо її в подання
            return View(new VisitorViewModel());
        }

        // POST: /Home/Index - обробляємо дані, надіслані з форми
        [HttpPost]
        public IActionResult Index(VisitorViewModel model)
        {
            // Model binding автоматично зв'язує дані форми з моделлю
            // За бажанням можна перевірити ModelState.IsValid
            return View(model);
        }
    }
}
