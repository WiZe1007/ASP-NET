using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_3.Data;
using Task_3.Models;
using System.Text.Json;

namespace Task_3.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _path;
        public MessagesController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _path = Path.Combine(env.ContentRootPath, "App_Data", "messages.json");
            var dir = Path.GetDirectoryName(_path)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!System.IO.File.Exists(_path)) System.IO.File.WriteAllText(_path, "[]");
        }

        [HttpGet]
        public IActionResult Send()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");
            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public IActionResult Send(SendMessageViewModel vm)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");
            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            if (!ModelState.IsValid) return View(vm);

            var json = System.IO.File.ReadAllText(_path);
            var list = JsonSerializer.Deserialize<List<Message>>(json)!;
            var msg = new Message
            {
                Id = Guid.NewGuid(),
                From = user,
                To = vm.To,
                Theme = vm.Theme,
                Text = vm.Text,
                SentAt = DateTime.Now
            };
            list.Add(msg);
            System.IO.File.WriteAllText(_path,
              JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));

            return RedirectToAction("Sent");
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            var sent = list.Where(m => m.From == user).OrderByDescending(m => m.SentAt).ToList();
            return View(sent);
        }

        [HttpGet]
        public IActionResult Inbox()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            var inbox = list.Where(m => m.To == user).OrderByDescending(m => m.SentAt).ToList();
            return View(inbox);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            var msg = list.FirstOrDefault(m => m.Id == id);
            if (msg == null || (msg.To != user && msg.From != user))
                return NotFound();
            return View(msg);
        }
    }
}
