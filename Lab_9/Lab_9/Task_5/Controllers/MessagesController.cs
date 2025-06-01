using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_5.Data;
using Task_5.Models;
using System.Text.Json;

namespace Task_5.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _filePath;

        public MessagesController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _filePath = Path.Combine(env.ContentRootPath, "App_Data", "messages.json");
            var dir = Path.GetDirectoryName(_filePath)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!System.IO.File.Exists(_filePath)) System.IO.File.WriteAllText(_filePath, "[]");
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
            var from = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(from)) return RedirectToAction("Login", "Account");
            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            if (!ModelState.IsValid) return View(vm);

            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_filePath))!;
            list.Add(new Message
            {
                Id = Guid.NewGuid(),
                From = from,
                To = vm.To,
                Theme = vm.Theme,
                Text = vm.Text,
                SentAt = DateTime.Now
            });
            System.IO.File.WriteAllText(_filePath,
              JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
            return RedirectToAction("Sent");
        }

        [HttpGet]
        public IActionResult Inbox()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");
            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_filePath))!;
            var inbox = list.Where(m => m.To == user).OrderByDescending(m => m.SentAt).ToList();
            return View(inbox);
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");
            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_filePath))!;
            var sent = list.Where(m => m.From == user).OrderByDescending(m => m.SentAt).ToList();
            return View(sent);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user)) return RedirectToAction("Login", "Account");
            var list = JsonSerializer.Deserialize<List<Message>>(System.IO.File.ReadAllText(_filePath))!;
            var msg = list.FirstOrDefault(m => m.Id == id);
            if (msg == null || (msg.To != user && msg.From != user))
                return NotFound();
            return View(msg);
        }
    }
}
