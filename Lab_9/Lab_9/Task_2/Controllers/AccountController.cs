using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_2.Data;
using Task_2.Models;
using System.Linq;

namespace Task_2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (_db.Users.Any(u => u.Name == vm.Name))
                ModelState.AddModelError(nameof(vm.Name), "Це ім’я вже зайняте");

            if (!ModelState.IsValid)
                return View(vm);

            _db.Users.Add(new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            });
            _db.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _db.Users
                          .FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Невірні облікові дані");
                return View(vm);
            }

            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var user = _db.Users.FirstOrDefault(u => u.Name == username);
            if (user == null)
                return RedirectToAction("Login");

            return View(user);
        }

        // Edit actions (EditName, EditEmail, EditPhone, EditPassword) — як у Task_1
        // …
    }
}
