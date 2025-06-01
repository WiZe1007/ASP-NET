using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_4.Data;
using Task_4.Models;
using System.Linq;

namespace Task_4.Controllers
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
            if (!ModelState.IsValid) return View(vm);

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
            if (!ModelState.IsValid) return View(vm);

            var user = _db.Users.FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);
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
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");

            var user = _db.Users.FirstOrDefault(u => u.Name == name);
            return user == null ? RedirectToAction("Login") : View(user);
        }

        [HttpGet] public IActionResult EditName() { /* … */ return View(); }
        [HttpPost] public IActionResult EditName(EditNameViewModel vm) { /* … */ return RedirectToAction("Profile"); }

        [HttpGet] public IActionResult EditEmail() { /* … */ return View(); }
        [HttpPost] public IActionResult EditEmail(EditEmailViewModel vm) { /* … */ return RedirectToAction("Profile"); }

        [HttpGet] public IActionResult EditPhone() { /* … */ return View(); }
        [HttpPost] public IActionResult EditPhone(EditPhoneViewModel vm) { /* … */ return RedirectToAction("Profile"); }

        [HttpGet] public IActionResult EditPassword() { /* … */ return View(); }
        [HttpPost] public IActionResult EditPassword(EditPasswordViewModel vm) { /* … */ return RedirectToAction("Profile"); }
    }
}
