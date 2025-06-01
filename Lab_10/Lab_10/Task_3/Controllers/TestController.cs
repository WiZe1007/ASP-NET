using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Task_3.Models;  

namespace Task_3.Controllers
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

            using (var cmd = new SqlCommand("SELECT * FROM Questions", cn))
            using (var r = cmd.ExecuteReader())
            {
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

            // --- Ось тут рандомізуємо порядок питань ---
            var rnd = new Random();
            list = list
                .OrderBy(_ => rnd.Next())    // або OrderBy(_ => Guid.NewGuid())
                .ToList();

            return View(list);
        }

        [HttpPost]
        public IActionResult Take(List<TestAnswerViewModel> answers)
        {
            int score = 0;
            foreach (var q in answers)
            {
                switch (q.QuestionType)
                {
                    case 'R':
                        if (q.SelectedOption.HasValue &&
                            q.CorrectAnswers.Equals(q.SelectedOption.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            score++;
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
                            score++;
                        break;

                    case 'T':
                        if (!string.IsNullOrWhiteSpace(q.TextAnswer) &&
                            string.Equals(q.TextAnswer.Trim(), q.CorrectAnswers.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            score++;
                        }
                        break;
                }
            }

            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            using var cn = new SqlConnection(_conn);
            cn.Open();

            using var cmd = new SqlCommand(
                @"INSERT INTO TestResults (UserName, Score, TakenAt)
                  VALUES (@user, @score, @time)", cn);

            cmd.Parameters.AddWithValue("@user", userName);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@time", DateTime.Now);

            cmd.ExecuteNonQuery();

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
