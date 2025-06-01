using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Task_2.Data;
using Task_2.Models;
using System.Text.Json;

namespace Task_2.Controllers
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
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!System.IO.File.Exists(_filePath))
                System.IO.File.WriteAllText(_filePath, "[]");
        }

        [HttpGet]
        public IActionResult Send()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public IActionResult Send(SendMessageViewModel vm)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            ViewBag.Users = _db.Users.Select(u => u.Name).ToList();
            if (!ModelState.IsValid)
                return View(vm);

            var json = System.IO.File.ReadAllText(_filePath);
            var messages = JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();

            messages.Add(new Message
            {
                From = username,
                To = vm.To,
                Theme = vm.Theme,
                Text = vm.Text,
                SentAt = DateTime.Now
            });

            System.IO.File.WriteAllText(_filePath,
                JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true }));

            return RedirectToAction("Sent");
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var json = System.IO.File.ReadAllText(_filePath);
            var messages = JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();

            var sent = messages
                .Where(m => m.From == username)
                .OrderByDescending(m => m.SentAt)
                .ToList();

            return View(sent);
        }
    }
}
