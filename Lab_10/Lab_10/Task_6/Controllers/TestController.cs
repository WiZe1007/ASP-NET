// Controllers/TestController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Task_6.Models;
using Task_6.Services;

namespace Task_6.Controllers
{
    public class TestController : Controller
    {
        private readonly string _conn;
        private readonly FileStore<User> _users;
        private readonly SettingsService _settings;

        public TestController(
            IConfiguration cfg,
            FileStore<User> users,
            SettingsService settings)
        {
            _conn = cfg.GetConnectionString("TestDb")!;
            _users = users;
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // 1) Перевірка, чи залогінений
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            // 2) Порахувати використані спроби з БД
            int used;
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM TestResults WHERE UserName = @user", cn);
                cmd.Parameters.AddWithValue("@user", userName);
                used = (int)cmd.ExecuteScalar()!;
            }

            // 3) Отримати динамічний ліміт із налаштувань
            int allowed = _settings.Load().AllowedAttempts;

            // 4) Передати до View
            ViewBag.AttemptsLeft = Math.Max(0, allowed - used);
            ViewBag.AllowedAttempts = allowed;
            return View();
        }

        [HttpGet]
        public IActionResult Take()
        {
            // 1) Авторизація
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            // 2) Порахувати вже використані спроби
            int used;
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM TestResults WHERE UserName = @user", cn);
                cmd.Parameters.AddWithValue("@user", userName);
                used = (int)cmd.ExecuteScalar()!;
            }

            // 3) Динамічний ліміт
            int allowed = _settings.Load().AllowedAttempts;
            if (used >= allowed)
                return RedirectToAction("Index");

            // 4) Завантажити та перемішати питання
            var list = new List<TestAnswerViewModel>();
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using var cmd = new SqlCommand("SELECT * FROM Questions", cn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new TestAnswerViewModel
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
            }
            list = list.OrderBy(_ => Guid.NewGuid()).ToList();

            return View(list);
        }

        [HttpPost]
        public IActionResult Take(List<TestAnswerViewModel> answers)
        {
            // 1) Авторизація
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            // 2) Перевірити ліміт знову
            int used;
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM TestResults WHERE UserName = @user", cn);
                cmd.Parameters.AddWithValue("@user", userName);
                used = (int)cmd.ExecuteScalar()!;
            }
            int allowed = _settings.Load().AllowedAttempts;
            if (used >= allowed)
                return RedirectToAction("Index");

            // 3) Порахувати правильні відповіді
            int correctCount = 0;
            foreach (var q in answers)
            {
                switch (q.QuestionType)
                {
                    case 'R':
                        if (q.SelectedOption.HasValue &&
                            string.Equals(
                                q.SelectedOption.Value.ToString(),
                                q.CorrectAnswers,
                                StringComparison.OrdinalIgnoreCase))
                        {
                            correctCount++;
                        }
                        break;

                    case 'C':
                        var correctSet = q.CorrectAnswers
                            .ToUpper().ToCharArray()
                            .Select(c => c.ToString())
                            .OrderBy(s => s);
                        var userSet = q.SelectedOptions
                            .Select(s => s.ToUpper())
                            .OrderBy(s => s);
                        if (correctSet.SequenceEqual(userSet))
                            correctCount++;
                        break;

                    case 'T':
                        if (!string.IsNullOrWhiteSpace(q.TextAnswer) &&
                            string.Equals(
                                q.TextAnswer.Trim(),
                                q.CorrectAnswers.Trim(),
                                StringComparison.OrdinalIgnoreCase))
                        {
                            correctCount++;
                        }
                        break;
                }
            }

            // 4) Розрахувати оцінку 0–100
            int total = answers.Count;
            int percentage = total > 0
                ? (int)Math.Round(correctCount * 100.0 / total)
                : 0;

            // 5) Зберегти в TestResults
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using var cmd = new SqlCommand(
                    @"INSERT INTO TestResults (UserName, Score, TakenAt)
                      VALUES (@user, @score, @time)", cn);
                cmd.Parameters.AddWithValue("@user", userName);
                cmd.Parameters.AddWithValue("@score", percentage);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            // 6) Оновити JSON-профіль
            var allUsers = _users.Load();
            var me = allUsers.First(u => u.Name == userName);
            me.Score = percentage;
            _users.Save(allUsers);

            return RedirectToAction("Results", new { score = percentage });
        }

        [HttpGet]
        public IActionResult Results(int score)
        {
            ViewBag.Score = score;
            return View();
        }
    }
}
