// Controllers/TestController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class TestController : Controller
    {
        private readonly string _conn;

        public TestController(IConfiguration cfg)
        {
            _conn = cfg.GetConnectionString("TestDb")!;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

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

            return View(list);
        }

        [HttpPost]
        public IActionResult Take(List<TestAnswerViewModel> answers)
        {
            // 1) Порахувати бали
            int score = 0;
            foreach (var q in answers)
            {
                switch (q.QuestionType)
                {
                    case 'R':  // одиночний вибір
                        if (q.SelectedOption.HasValue &&
                            q.CorrectAnswers.Equals(q.SelectedOption.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            score++;
                        }
                        break;

                    case 'C':  // множинний вибір
                        var correctSet = q.CorrectAnswers
                            .ToUpper()
                            .ToCharArray()
                            .Select(c => c.ToString())
                            .OrderBy(s => s);
                        var userSet = q.SelectedOptions
                            .Select(s => s.ToUpper())
                            .OrderBy(s => s);

                        if (correctSet.SequenceEqual(userSet))
                        {
                            score++;
                        }
                        break;

                    case 'T':  // текстова відповідь
                        if (!string.IsNullOrWhiteSpace(q.TextAnswer) &&
                            string.Equals(q.TextAnswer.Trim(), q.CorrectAnswers.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            score++;
                        }
                        break;
                }
            }

            // 2) Отримати ім'я користувача
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            // 3) Зберегти результат у БД
            using var cn = new SqlConnection(_conn);
            cn.Open();

            using var cmd = new SqlCommand(
                @"INSERT INTO TestResults (UserName, Score, TakenAt)
                  VALUES (@user, @score, @time)", cn);

            cmd.Parameters.AddWithValue("@user", userName);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@time", DateTime.Now);

            cmd.ExecuteNonQuery();

            // 4) Показати сторінку з результатом
            return RedirectToAction("Results", new { score });
        }

        [HttpGet]
        public IActionResult Results(int score)
        {
            ViewBag.Score = score;
            return View();
        }
    }
}
