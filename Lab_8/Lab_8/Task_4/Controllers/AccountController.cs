using Microsoft.AspNetCore.Mvc;
using Task_4.Data;
using Task_4.Models;
using System.Linq;

namespace Task_4.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) => _db = db;  // Впровадження контексту БД

        // REGISTER
        [HttpGet]
        public IActionResult Register() => View();  // Відображення форми реєстрації

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (_db.Users.Any(u => u.Name == vm.Name))
                ModelState.AddModelError(nameof(vm.Name), "This Name is taken");  // Перевірка унікальності імені

            if (!ModelState.IsValid)
                return View(vm);  // Повернення форми з помилками

            _db.Users.Add(new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            });
            _db.SaveChanges();  // Збереження даних користувача в БД

            return RedirectToAction("Login");  // Перенаправлення на форму входу
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login() => View();  // Відображення форми входу

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);  // Якщо форма не валідна, повертаємо її

            var user = _db.Users
                .FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);  // Перевірка користувача

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");  // Якщо не знайдено користувача
                return View(vm);
            }

            HttpContext.Session.SetString("UserName", user.Name);  // Збереження імені користувача в сесії
            return RedirectToAction("Index", "Home");  // Перенаправлення на головну сторінку
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Очистка сесії
            return RedirectToAction("Login");  // Перенаправлення на форму входу
        }
    }
}
