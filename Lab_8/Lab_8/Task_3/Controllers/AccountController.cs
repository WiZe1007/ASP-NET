using Microsoft.AspNetCore.Mvc;
using Task_3.Data;
using Task_3.Models;
using System.Linq;

namespace Task_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) => _db = db;  // Впровадження контексту

        // ==== REGISTER ====
        [HttpGet]
        public IActionResult Register() => View();  // Показати форму реєстрації

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (_db.Users.Any(u => u.Name == vm.Name))
                ModelState.AddModelError(nameof(vm.Name), "This Name is taken");  // Перевірка на унікальність імені

            if (!ModelState.IsValid)
                return View(vm);  // Якщо форма не валідна, повертаємо її

            _db.Users.Add(new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            });
            _db.SaveChanges();  // Зберігаємо новий користувач у базі даних

            return RedirectToAction("Login");  // Перенаправляємо на сторінку входу
        }

        // ==== LOGIN ====
        [HttpGet]
        public IActionResult Login() => View();  // Показати форму входу

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);  // Якщо форма не валідна, повертаємо її

            var user = _db.Users
                .FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);  // Перевірка на існування користувача

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");  // Якщо користувача немає
                return View(vm);
            }

            HttpContext.Session.SetString("UserName", user.Name);  // Зберігаємо ім’я користувача в сесії
            return RedirectToAction("Index", "Home");  // Перенаправляємо на домашню сторінку
        }
    }
}
