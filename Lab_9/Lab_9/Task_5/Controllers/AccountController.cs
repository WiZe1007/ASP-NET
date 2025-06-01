using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_5.Data;
using Task_5.Models;
using System.Linq;

namespace Task_5.Controllers
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
                ModelState.AddModelError(nameof(vm.Name), "Name is taken");
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

            var user = _db.Users
                .FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
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
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");
            var user = _db.Users.FirstOrDefault(u => u.Name == name);
            return user == null ? RedirectToAction("Login") : View(user);
        }

        [HttpGet]
        public IActionResult EditName()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            var user = _db.Users.First(u => u.Name == name);
            return View(new EditNameViewModel { Name = user.Name });
        }

        [HttpPost]
        public IActionResult EditName(EditNameViewModel vm)
        {
            var oldName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(oldName)) return RedirectToAction("Login");

            if (_db.Users.Any(u => u.Name == vm.Name && u.Name != oldName))
                ModelState.AddModelError(nameof(vm.Name), "Name is taken");
            if (!ModelState.IsValid) return View(vm);

            var user = _db.Users.First(u => u.Name == oldName);
            user.Name = vm.Name;
            _db.SaveChanges();
            HttpContext.Session.SetString("UserName", vm.Name);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult EditEmail()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            var user = _db.Users.First(u => u.Name == name);
            return View(new EditEmailViewModel { Email = user.Email });
        }

        [HttpPost]
        public IActionResult EditEmail(EditEmailViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            if (!ModelState.IsValid) return View(vm);

            var user = _db.Users.First(u => u.Name == name);
            user.Email = vm.Email;
            _db.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult EditPhone()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            var user = _db.Users.First(u => u.Name == name);
            return View(new EditPhoneViewModel { Phone = user.Phone });
        }

        [HttpPost]
        public IActionResult EditPhone(EditPhoneViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            if (!ModelState.IsValid) return View(vm);

            var user = _db.Users.First(u => u.Name == name);
            user.Phone = vm.Phone;
            _db.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult EditPassword()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public IActionResult EditPassword(EditPasswordViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login");
            if (!ModelState.IsValid) return View(vm);

            var user = _db.Users.First(u => u.Name == name);
            user.Password = vm.NewPassword;
            _db.SaveChanges();
            return RedirectToAction("Profile");
        }
    }
}
