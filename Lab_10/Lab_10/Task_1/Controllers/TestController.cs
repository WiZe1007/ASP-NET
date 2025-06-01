using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Task_1.Models;

namespace Task_1.Controllers
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
            => View();

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
                    CorrectOption = ((string)r["CorrectOption"])[0]
                });
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Take(List<TestAnswerViewModel> answers)
        {
            // Порахувати бали, порівнюючи заповнене з прихованим CorrectOption
            int score = 0;
            foreach (var ans in answers)
            {
                if (ans.SelectedOption.HasValue
                    && ans.SelectedOption.Value == ans.CorrectOption)
                {
                    score++;
                }
            }

            var user = HttpContext.Session.GetString("UserName")!;
            using var cn = new SqlConnection(_conn);
            cn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO TestResults(UserName,Score,TakenAt) VALUES(@u,@s,@t)", cn);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@s", score);
            cmd.Parameters.AddWithValue("@t", DateTime.Now);
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
