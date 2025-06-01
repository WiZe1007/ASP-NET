using Microsoft.AspNetCore.Mvc;
using Task_5.Models;
using Task_5.Services;

namespace Task_5.Controllers
{
    public class MessagesController : Controller
    {
        private readonly FileStore<Message> _msgs;
        private readonly FileStore<User> _users;

        public MessagesController(FileStore<Message> msgs, FileStore<User> users)
        {
            _msgs = msgs;
            _users = users;
        }

        [HttpGet]
        public IActionResult Send()
        {
            var from = HttpContext.Session.GetString("UserName");
            if (from == null) return RedirectToAction("Login", "Account");

            ViewBag.Users = _users.Load().Select(u => u.Name).ToList();
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public IActionResult Send(SendMessageViewModel vm)
        {
            var from = HttpContext.Session.GetString("UserName");
            if (from == null) return RedirectToAction("Login", "Account");

            var list = _msgs.Load();
            list.Add(new Message
            {
                Id = Guid.NewGuid(),
                From = from,
                To = vm.To,
                Theme = vm.Theme,
                Text = vm.Text,
                SentAt = DateTime.Now
            });
            _msgs.Save(list);
            return RedirectToAction("Sent");
        }

        [HttpGet]
        public IActionResult Inbox()
        {
            var me = HttpContext.Session.GetString("UserName");
            if (me == null) return RedirectToAction("Login", "Account");

            var inbox = _msgs.Load()
                .Where(m => m.To == me)
                .OrderByDescending(m => m.SentAt)
                .ToList();
            return View(inbox);
        }

        [HttpGet]
        public IActionResult Sent()
        {
            var me = HttpContext.Session.GetString("UserName");
            if (me == null) return RedirectToAction("Login", "Account");

            var sent = _msgs.Load()
                .Where(m => m.From == me)
                .OrderByDescending(m => m.SentAt)
                .ToList();
            return View(sent);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var me = HttpContext.Session.GetString("UserName");
            if (me == null) return RedirectToAction("Login", "Account");

            var msg = _msgs.Load().FirstOrDefault(m => m.Id == id);
            if (msg == null || (msg.From != me && msg.To != me))
                return NotFound();

            return View(msg);
        }
    }
}
