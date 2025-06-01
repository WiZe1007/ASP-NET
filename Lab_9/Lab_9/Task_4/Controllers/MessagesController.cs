using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_4.Data;
using Task_4.Models;
using System.Text.Json;

namespace Task_4.Controllers
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
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login", "Account");

            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public IActionResult Send(SendMessageViewModel vm)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login", "Account");

            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            if (!ModelState.IsValid) return View(vm);

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            list.Add(new Message
            {
                Id = Guid.NewGuid(),
                From = name,
                To = vm.To,
                Theme = vm.Theme,
                Text = vm.Text,
                SentAt = DateTime.Now
            });
            System.IO.File.WriteAllText(_path,
              JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));

            return RedirectToAction("Sent");
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login", "Account");

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            var sent = list.Where(m => m.From == name)
                           .OrderByDescending(m => m.SentAt)
                           .ToList();
            return View(sent);
        }

        [HttpGet]
        public IActionResult Inbox()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login", "Account");

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!;
            var inbox = list.Where(m => m.To == name)
                            .OrderByDescending(m => m.SentAt)
                            .ToList();
            return View(inbox);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Login", "Account");

            var msg = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_path))!
                          .FirstOrDefault(m => m.Id == id);
            if (msg == null || (msg.To != name && msg.From != name))
                return NotFound();
            return View(msg);
        }
    }
}
