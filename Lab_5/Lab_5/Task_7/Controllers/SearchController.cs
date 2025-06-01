using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Task_7.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _configuration;
        public SearchController(IConfiguration configuration)
        {
            // Зберігаємо конфігурацію для подальшого доступу до рядка підключення
            _configuration = configuration;
        }

        // View для введення Паролю
        [HttpGet]
        public IActionResult Password(bool reset = false)
        {
            // Якщо користувач вирішив почати знову – очищаємо сесію та помилки
            if (reset)
            {
                HttpContext.Session.Clear();
                TempData["Error"] = null;
            }
            // Встановлюємо фон (CSS-клас) для цієї сторінки
            ViewBag.BodyClass = "bg1";
            // Заголовок для цієї View
            ViewData["Title"] = "Введіть пароль для пошуку";
            return View();
        }

        [HttpPost]
        public IActionResult Password(string password)
        {
            // Перевірка – якщо пароль не введено, повертаємо форму з помилкою
            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Будь ласка, введіть пароль.");
                return View();
            }
            // Зберігаємо введений пароль у сесії
            HttpContext.Session.SetString("SearchPassword", password);
            return RedirectToAction("Email");
        }

        // View для введення Email
        [HttpGet]
        public IActionResult Email()
        {
            // Якщо пароль не заданий – повертаємо користувача на попередню сторінку
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchPassword")))
                return RedirectToAction("Password");

            // Встановлюємо фон для цієї сторінки (інший CSS-клас)
            ViewBag.BodyClass = "bg2";
            ViewData["Title"] = "Введіть Email для пошуку";
            return View();
        }

        [HttpPost]
        public IActionResult Email(string email)
        {
            // Перевірка введення Email
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Будь ласка, введіть Email.");
                return View();
            }
            // Зберігаємо Email у сесії
            HttpContext.Session.SetString("SearchEmail", email);

            // Отримуємо дані, що були введені раніше
            string searchPassword = HttpContext.Session.GetString("SearchPassword");
            string searchEmail = HttpContext.Session.GetString("SearchEmail");
            Account account = null;
            string connStr = _configuration.GetConnectionString("RegistrationDb");
            try
            {
                // Підключаємося до бази даних і виконуємо запит для пошуку акаунту
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 Id, Password, Email, CreatedAt FROM Accounts WHERE Password = @Password AND Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", searchPassword);
                        cmd.Parameters.AddWithValue("@Email", searchEmail);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заповнюємо об'єкт account даними з бази
                                account = new Account
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Якщо сталася помилка при пошуку, зберігаємо її повідомлення і повертаємо користувача на сторінку введення пароля
                TempData["Error"] = "Помилка пошуку: " + ex.Message;
                return RedirectToAction("Password", new { reset = true });
            }
            // Очищаємо сесію після завершення пошукового процесу
            HttpContext.Session.Clear();
            // Зберігаємо результат пошуку у TempData у форматі JSON для подальшого відображення
            TempData["AccountResult"] = JsonConvert.SerializeObject(account);
            return RedirectToAction("Result");
        }

        // View для відображення результату пошуку
        [HttpGet]
        public IActionResult Result()
        {
            // Встановлюємо фон для сторінки результату
            ViewBag.BodyClass = "bgResult";
            ViewData["Title"] = "Результат пошуку";
            string json = TempData["AccountResult"] as string;
            Account account = null;
            if (!string.IsNullOrEmpty(json))
            {
                // Десеріалізуємо JSON-рядок у об'єкт Account
                account = JsonConvert.DeserializeObject<Account>(json);
            }
            return View(account);
        }
    }

    // Модель, що відповідає структурі таблиці Accounts
    public class Account
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
