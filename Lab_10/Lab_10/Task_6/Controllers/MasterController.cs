// Controllers/MasterController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Task_6.Models;
using Task_6.Services;

namespace Task_6.Controllers
{
    public class MasterController : Controller
    {
        private readonly FileStore<User> _users;
        private readonly SettingsService _settings;
        private readonly string _conn;

        public MasterController(
            FileStore<User> users,
            SettingsService settings,
            IConfiguration cfg)
        {
            _users = users;
            _settings = settings;
            _conn = cfg.GetConnectionString("TestDb")!;
        }

        // Захист: лише Master
        private bool IsMaster() =>
            HttpContext.Session.GetString("UserRole") == "Master";

        private IActionResult Denied() =>
            RedirectToAction("Index", "Account");

        // 1) Список питань
        [HttpGet]
        public IActionResult Questions()
        {
            if (!IsMaster()) return Denied();
            var list = new List<TestQuestion>();
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand("SELECT * FROM Questions", cn);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new TestQuestion
                {
                    Id = (int)r["Id"],
                    Text = (string)r["Text"],
                    OptionA = (string)r["OptionA"],
                    OptionB = (string)r["OptionB"],
                    OptionC = (string)r["OptionC"],
                    OptionD = (string)r["OptionD"],
                    QuestionType = ((string)r["QuestionType"])[0],
                    CorrectAnswers = (string)r["CorrectAnswers"]
                });
            }
            return View(list);
        }

        // 2) Створити нове питання
        [HttpGet]
        public IActionResult CreateQuestion()
        {
            if (!IsMaster()) return Denied();
            return View();
        }

        [HttpPost]
        public IActionResult CreateQuestion(TestQuestion q)
        {
            if (!IsMaster()) return Denied();
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO Questions
                 (Text,OptionA,OptionB,OptionC,OptionD,QuestionType,CorrectAnswers)
                  VALUES(@t,@a,@b,@c,@d,@type,@corr)", cn);
            cmd.Parameters.AddWithValue("@t", q.Text);
            cmd.Parameters.AddWithValue("@a", q.OptionA);
            cmd.Parameters.AddWithValue("@b", q.OptionB);
            cmd.Parameters.AddWithValue("@c", q.OptionC);
            cmd.Parameters.AddWithValue("@d", q.OptionD);
            cmd.Parameters.AddWithValue("@type", q.QuestionType.ToString());
            cmd.Parameters.AddWithValue("@corr", q.CorrectAnswers);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Questions");
        }

        // 3) Редагувати питання
        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            if (!IsMaster()) return Denied();
            TestQuestion q = null!;
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand(
                "SELECT * FROM Questions WHERE Id=@id", cn);
            cmd.Parameters.AddWithValue("@id", id);
            using var r = cmd.ExecuteReader();
            if (r.Read()) q = new TestQuestion
            {
                Id = id,
                Text = (string)r["Text"],
                OptionA = (string)r["OptionA"],
                OptionB = (string)r["OptionB"],
                OptionC = (string)r["OptionC"],
                OptionD = (string)r["OptionD"],
                QuestionType = ((string)r["QuestionType"])[0],
                CorrectAnswers = (string)r["CorrectAnswers"]
            };
            return View(q);
        }

        [HttpPost]
        public IActionResult EditQuestion(TestQuestion q)
        {
            if (!IsMaster()) return Denied();
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE Questions SET
                  Text=@t, OptionA=@a, OptionB=@b, OptionC=@c, OptionD=@d,
                  QuestionType=@type, CorrectAnswers=@corr
                  WHERE Id=@id", cn);
            cmd.Parameters.AddWithValue("@t", q.Text);
            cmd.Parameters.AddWithValue("@a", q.OptionA);
            cmd.Parameters.AddWithValue("@b", q.OptionB);
            cmd.Parameters.AddWithValue("@c", q.OptionC);
            cmd.Parameters.AddWithValue("@d", q.OptionD);
            cmd.Parameters.AddWithValue("@type", q.QuestionType.ToString());
            cmd.Parameters.AddWithValue("@corr", q.CorrectAnswers);
            cmd.Parameters.AddWithValue("@id", q.Id);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Questions");
        }

        // 4) Видалити питання
        [HttpPost]
        public IActionResult DeleteQuestion(int id)
        {
            if (!IsMaster()) return Denied();
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand(
                "DELETE FROM Questions WHERE Id=@id", cn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Questions");
        }

        // 5) Налаштування ліміту спроб
        [HttpGet]
        public IActionResult Settings()
        {
            if (!IsMaster()) return Denied();
            var m = _settings.Load();
            return View(m);
        }

        [HttpPost]
        public IActionResult Settings(TestSettings m)
        {
            if (!IsMaster()) return Denied();
            _settings.Save(m);
            return RedirectToAction("Settings");
        }

        // 6) Перегляд оцінок користувачів
        [HttpGet]
        public IActionResult Scores()
        {
            if (!IsMaster()) return Denied();
            var users = _users.Load()
                              .OrderByDescending(u => u.Score)
                              .ToList();
            return View(users);
        }
    }
}
