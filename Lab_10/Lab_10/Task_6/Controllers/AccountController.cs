// Controllers/AccountController.cs
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_6.Models;
using Task_6.Services;

namespace Task_6.Controllers
{
    public class AccountController : Controller
    {
        private readonly FileStore<User> _users;

        public AccountController(FileStore<User> users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            var all = _users.Load();
            if (all.Any(u => u.Name == vm.Name))
                ModelState.AddModelError(nameof(vm.Name), "Name taken");
            if (!ModelState.IsValid)
                return View(vm);

            all.Add(new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password,
                Score = 0,
                Role = "User"
            });
            _users.Save(all);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _users
                .Load()
                .FirstOrDefault(u => u.Name == vm.Name && u.Password == vm.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(vm);
            }

            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role);

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

            var user = _users.Load().First(u => u.Name == name);
            return View(user);
        }

        [HttpGet]
        public IActionResult EditName()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");

            var user = _users.Load().First(u => u.Name == name);
            return View(new EditNameViewModel { Name = user.Name });
        }

        [HttpPost]
        public IActionResult EditName(EditNameViewModel vm)
        {
            var old = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(old))
                return RedirectToAction("Login");

            var all = _users.Load();
            if (all.Any(u => u.Name == vm.Name && u.Name != old))
                ModelState.AddModelError(nameof(vm.Name), "Name taken");
            if (!ModelState.IsValid)
                return View(vm);

            var user = all.First(u => u.Name == old);
            user.Name = vm.Name;
            _users.Save(all);

            HttpContext.Session.SetString("UserName", vm.Name);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult EditEmail()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");

            var user = _users.Load().First(u => u.Name == name);
            return View(new EditEmailViewModel { Email = user.Email });
        }

        [HttpPost]
        public IActionResult EditEmail(EditEmailViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");
            if (!ModelState.IsValid)
                return View(vm);

            var all = _users.Load();
            var user = all.First(u => u.Name == name);
            user.Email = vm.Email;
            _users.Save(all);

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult EditPhone()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");

            var user = _users.Load().First(u => u.Name == name);
            return View(new EditPhoneViewModel { Phone = user.Phone });
        }

        [HttpPost]
        public IActionResult EditPhone(EditPhoneViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");
            if (!ModelState.IsValid)
                return View(vm);

            var all = _users.Load();
            var user = all.First(u => u.Name == name);
            user.Phone = vm.Phone;
            _users.Save(all);

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
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Login");
            if (!ModelState.IsValid)
                return View(vm);

            var all = _users.Load();
            var user = all.First(u => u.Name == name);
            user.Password = vm.NewPassword;
            _users.Save(all);

            return RedirectToAction("Profile");
        }
    }
}
