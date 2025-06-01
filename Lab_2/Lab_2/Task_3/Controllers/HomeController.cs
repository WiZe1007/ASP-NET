using Microsoft.AspNetCore.Mvc;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        // GET-запит: просто повертаЇмо форму без попередн≥х даних
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST-запит: отримуЇмо дан≥ з форми
        // ѕараметри Name, Phone, Email, Birthdate в≥дпов≥дають ≥менам пол≥в форми,
        // а параметр action визначаЇ, €ку кнопку було натиснуто (submit або reset)
        [HttpPost]
        public IActionResult Index(string Name, string Phone, string Email, string Birthdate, string action)
        {
            if (action == "reset")
            {
                // якщо натиснуто "—кинути", очищуЇмо вс≥ пол€
                ViewBag.Name = "";
                ViewBag.Phone = "";
                ViewBag.Email = "";
                ViewBag.Birthdate = "";
            }
            else
            {
                // якщо натиснуто "¬≥д≥слати", збер≥гаЇмо отриман≥ дан≥ дл€ подальшого в≥дображенн€
                ViewBag.Name = Name;
                ViewBag.Phone = Phone;
                ViewBag.Email = Email;
                ViewBag.Birthdate = Birthdate;
            }

            return View(); // ѕовертаЇмо форму з оновленими даними
        }
    }
}
