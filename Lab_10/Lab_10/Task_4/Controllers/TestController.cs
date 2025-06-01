// Controllers/TestController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Task_4.Models;
using Task_4.Services;   // для FileStore<User>

namespace Task_4.Controllers
{
    public class TestController : Controller
    {
        private readonly string _conn;
        private readonly FileStore<User> _users;

        public TestController(IConfiguration cfg, FileStore<User> users)
        {
            _conn = cfg.GetConnectionString("TestDb")!;
            _users = users;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Take()
        {
            var list = new List<TestAnswerViewModel>();
            using var cn = new SqlConnection(_conn);
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

            // Випадковий порядок питань
            var rnd = new Random();
            list = list.OrderBy(_ => rnd.Next()).ToList();

            return View(list);
        }

        [HttpPost]
        public IActionResult Take(List<TestAnswerViewModel> answers)
        {
            // 1) Порахувати кількість правильних відповідей
            int correctCount = 0;
            foreach (var q in answers)
            {
                switch (q.QuestionType)
                {
                    case 'R':
                        if (q.SelectedOption.HasValue &&
                            q.CorrectAnswers.Equals(
                              q.SelectedOption.Value.ToString(),
                              StringComparison.OrdinalIgnoreCase))
                            correctCount++;
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
                            correctCount++;
                        break;
                }
            }

            // 2) Перевести в оцінку 0–100
            int total = answers.Count;
            int percentage = total > 0
                ? (int)Math.Round(correctCount * 100.0 / total)
                : 0;

            // 3) Отримати ім'я з сесії
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            // 4) Зберегти в TestResults (DB)
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

            // 5) Оновити поле Score у профілі (JSON через FileStore<User>)
            var allUsers = _users.Load();
            var me = allUsers.First(u => u.Name == userName);
            me.Score = percentage;
            _users.Save(allUsers);

            // 6) Показати результат
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
