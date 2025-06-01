using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Task_8.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _configuration;
        public SearchController(IConfiguration configuration)
        {
            // Зберігаємо конфігурацію для роботи з базою даних
            _configuration = configuration;
        }

        // GET: Search/Index
        [HttpGet]
        public IActionResult Index(bool reset = false)
        {
            // Якщо користувач хоче розпочати новий пошук – очищуємо сесію та TempData
            if (reset)
            {
                HttpContext.Session.Clear();
                TempData["SearchResults"] = null;
                return RedirectToAction("Index");
            }

            // Якщо у TempData є результати пошуку, переходимо до етапу 3
            var tempResults = TempData["SearchResults"] as string;
            if (!string.IsNullOrEmpty(tempResults))
            {
                ViewBag.Stage = 3;
                // Десеріалізуємо результати з JSON-рядка у список акаунтів
                var results = JsonConvert.DeserializeObject<List<Account>>(tempResults);
                ViewBag.SearchResults = results;
                // Встановлюємо фон для результату
                ViewBag.BodyClass = "bg2";
                ViewBag.Title = "Результати пошуку";
                ViewBag.Prompt = "";
                return View();
            }

            // Якщо результатів немає, визначаємо етап за даними сесії
            int stage = 1;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchPassword")))
                stage = 1;
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchEmail")))
                stage = 2;
            ViewBag.Stage = stage;

            // Встановлюємо CSS-клас для фону залежно від етапу
            if (stage == 1)
                ViewBag.BodyClass = "bg1";
            else
                ViewBag.BodyClass = "bg2";

            // Встановлюємо заголовок та підказку для форми залежно від етапу
            if (stage == 1)
            {
                ViewBag.Title = "Введіть пароль для пошуку";
                ViewBag.Prompt = "Пароль:";
            }
            else if (stage == 2)
            {
                ViewBag.Title = "Введіть Email для пошуку";
                ViewBag.Prompt = "Email:";
            }
            ViewBag.Error = TempData["Error"];
            return View();
        }

        // POST: Search/Index
        [HttpPost]
        public IActionResult Index(string input)
        {
            // Етап 1: Якщо ще не введено пароль, зберігаємо введене значення як пароль
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchPassword")))
            {
                HttpContext.Session.SetString("SearchPassword", input);
                return RedirectToAction("Index");
            }
            // Етап 2: Якщо пароль введено, а Email ще не збережено – зберігаємо Email та виконуємо пошук
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchEmail")))
            {
                HttpContext.Session.SetString("SearchEmail", input);

                string searchPassword = HttpContext.Session.GetString("SearchPassword");
                string searchEmail = HttpContext.Session.GetString("SearchEmail");
                List<Account> accounts = new List<Account>();
                string connStr = _configuration.GetConnectionString("RegistrationDb");
                try
                {
                    // Підключаємося до бази та виконуємо запит для пошуку акаунтів
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string sql = "SELECT Id, Password, Email, CreatedAt FROM Accounts WHERE Password = @Password AND Email = @Email";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Password", searchPassword);
                            cmd.Parameters.AddWithValue("@Email", searchEmail);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                // Збираємо всі знайдені записи в список
                                while (reader.Read())
                                {
                                    accounts.Add(new Account
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        Password = reader.GetString(reader.GetOrdinal("Password")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Якщо виникла помилка під час пошуку, зберігаємо повідомлення і повертаємося на початок
                    TempData["Error"] = "Помилка пошуку: " + ex.Message;
                    return RedirectToAction("Index", new { reset = true });
                }
                // Очищаємо сесію після завершення пошуку, щоб уникнути повторного сабміту
                HttpContext.Session.Clear();
                // Зберігаємо результати пошуку у TempData у форматі JSON для подальшого відображення
                TempData["SearchResults"] = JsonConvert.SerializeObject(accounts);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

    // Клас моделі, що відповідає структурі таблиці Accounts
    public class Account
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
