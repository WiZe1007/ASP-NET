using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_3.Data;
using Task_3.Models;
using System.Linq;

namespace Task_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) => _db = db;

        [HttpGet] public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (_db.Users.Any(u => u.Name == vm.Name))
                ModelState.AddModelError(nameof(vm.Name), "Name taken");
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

        [HttpGet] public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var u = _db.Users.FirstOrDefault(x => x.Name == vm.Name && x.Password == vm.Password);
            if (u == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(vm);
            }
            HttpContext.Session.SetString("UserName", u.Name);
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

        [HttpGet] public IActionResult EditName() {/* аналогічно */ return View(); }
        [HttpPost] public IActionResult EditName(EditNameViewModel vm) {/*…*/ return RedirectToAction("Profile"); }
        [HttpGet] public IActionResult EditEmail() { return View(); }
        [HttpPost] public IActionResult EditEmail(EditEmailViewModel vm) { return RedirectToAction("Profile"); }
        [HttpGet] public IActionResult EditPhone() { return View(); }
        [HttpPost] public IActionResult EditPhone(EditPhoneViewModel vm) { return RedirectToAction("Profile"); }
        [HttpGet] public IActionResult EditPassword() { return View(); }
        [HttpPost] public IActionResult EditPassword(EditPasswordViewModel vm) { return RedirectToAction("Profile"); }
    }
}
