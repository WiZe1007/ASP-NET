using Microsoft.AspNetCore.Mvc;
using Task_2.Data;
using Task_2.Models;
using System.Linq;

namespace Task_2.Controllers
{
    // Контролер для реєстрації та обліку користувачів
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;  // Контекст БД

        public AccountController(ApplicationDbContext db) => _db = db;  // Впровадження контексту

        [HttpGet]
        public IActionResult Register() => View();  // Показати форму реєстрації

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            // Перевірка унікальності імені користувача
            if (_db.Users.Any(u => u.Name == model.Name))
                ModelState.AddModelError(nameof(model.Name), "This Name is taken");

            if (!ModelState.IsValid)
                return View(model);  // Повернути форму з повідомленнями про помилки

            // Додавання нового користувача в БД
            _db.Users.Add(new User
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password
            });
            _db.SaveChanges();  // Збереження змін

            return RedirectToAction("Register");  // Перенаправлення після успіху
        }
    }
}
