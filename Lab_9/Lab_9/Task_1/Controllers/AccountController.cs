using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_1.Data;
using Task_1.Models;
using System.Linq;

namespace Task_1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db) => _db = db;

        // REGISTER
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

        // LOGIN
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

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // PROFILE
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

        // EDIT NAME
        [HttpGet]
        public IActionResult EditName()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var user = _db.Users.First(u => u.Name == username);
            var vm = new EditNameViewModel { Name = user.Name };
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditName(EditNameViewModel vm)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            if (_db.Users.Any(u => u.Name == vm.Name && u.Name != username))
                ModelState.AddModelError(nameof(vm.Name), "Це ім’я вже зайняте");

            if (!ModelState.IsValid)
                return View(vm);

            var user = _db.Users.First(u => u.Name == username);
            user.Name = vm.Name;
            _db.SaveChanges();

            // Оновлюємо сессію
            HttpContext.Session.SetString("UserName", vm.Name);

            return RedirectToAction("Profile");
        }

        // EDIT EMAIL
        [HttpGet]
        public IActionResult EditEmail()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var user = _db.Users.First(u => u.Name == username);
            var vm = new EditEmailViewModel { Email = user.Email };
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditEmail(EditEmailViewModel vm)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(vm);

            var user = _db.Users.First(u => u.Name == username);
            user.Email = vm.Email;
            _db.SaveChanges();

            return RedirectToAction("Profile");
        }

        // EDIT PHONE
        [HttpGet]
        public IActionResult EditPhone()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var user = _db.Users.First(u => u.Name == username);
            var vm = new EditPhoneViewModel { Phone = user.Phone };
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditPhone(EditPhoneViewModel vm)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(vm);

            var user = _db.Users.First(u => u.Name == username);
            user.Phone = vm.Phone;
            _db.SaveChanges();

            return RedirectToAction("Profile");
        }

        // EDIT PASSWORD
        [HttpGet]
        public IActionResult EditPassword()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            return View(new EditPasswordViewModel());
        }

        [HttpPost]
        public IActionResult EditPassword(EditPasswordViewModel vm)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(vm);

            var user = _db.Users.First(u => u.Name == username);
            user.Password = vm.NewPassword;
            _db.SaveChanges();

            return RedirectToAction("Profile");
        }
    }
}
