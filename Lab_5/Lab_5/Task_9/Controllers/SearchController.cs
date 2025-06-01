using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json; 

namespace Task_9.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _configuration;
        public SearchController(IConfiguration configuration)
        {
            // Зберігаємо конфігурацію для доступу до рядка підключення до БД
            _configuration = configuration;
        }

        // Єдиний метод, що обробляє як GET, так і POST запити
        public IActionResult Index(string input = null, bool reset = false)
        {
            // Якщо користувач хоче почати новий пошук, очищуємо сесію і TempData
            if (reset)
            {
                HttpContext.Session.Clear();
                TempData["SearchResults"] = null;
                return RedirectToAction("Index");
            }

            // Якщо запит POST та значення введено – обробляємо дані
            if (Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(input))
            {
                // Етап 1: Якщо в сесії ще не збережено "SearchPassword", зберігаємо введене значення як пароль
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchPassword")))
                {
                    HttpContext.Session.SetString("SearchPassword", input);
                    return RedirectToAction("Index");
                }
                // Етап 2: Якщо пароль введено, а "SearchEmail" ще не збережено – зберігаємо email і виконуємо пошук
                else if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchEmail")))
                {
                    HttpContext.Session.SetString("SearchEmail", input);

                    string searchPassword = HttpContext.Session.GetString("SearchPassword");
                    string searchEmail = HttpContext.Session.GetString("SearchEmail");
                    List<Account> accounts = new List<Account>();
                    string connStr = _configuration.GetConnectionString("RegistrationDb");
                    try
                    {
                        // Підключаємось до бази і виконуємо SQL-запит
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
                                    // Збираємо всі знайдені записи
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
                        // Якщо виникла помилка, зберігаємо повідомлення та повертаємося до першого етапу
                        TempData["Error"] = "Помилка пошуку: " + ex.Message;
                        return RedirectToAction("Index", new { reset = true });
                    }
                    // Очищуємо сесію, щоб не повертатися до етапу вводу даних
                    HttpContext.Session.Clear();
                    // Зберігаємо результати пошуку у TempData у форматі JSON
                    TempData["SearchResults"] = JsonConvert.SerializeObject(accounts);
                    // Перехід до GET-логіки, яка встановить stage = 3 і відобразить результати
                    return RedirectToAction("Index");
                }
            }

            // Обробка GET-запиту:
            // Якщо у TempData є результати пошуку – встановлюємо stage = 3
            string tempJson = TempData["SearchResults"] as string;
            if (!string.IsNullOrEmpty(tempJson))
            {
                ViewBag.Stage = 3;
                var results = JsonConvert.DeserializeObject<List<Account>>(tempJson);
                ViewBag.SearchResults = results;
                ViewBag.BodyClass = "bg2";
                ViewBag.Title = "Результати пошуку";
                ViewBag.Prompt = "";
                return View();
            }

            // Якщо результатів немає, визначаємо stage за сесійними даними
            int stageVal = 1;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchPassword")))
            {
                stageVal = 1;
            }
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("SearchEmail")))
            {
                stageVal = 2;
            }
            ViewBag.Stage = stageVal;

            // Встановлюємо фон, заголовок і підказку залежно від stage
            if (stageVal == 1)
            {
                ViewBag.BodyClass = "bg1";
                ViewBag.Title = "Введіть пароль для пошуку";
                ViewBag.Prompt = "Пароль:";
            }
            else if (stageVal == 2)
            {
                ViewBag.BodyClass = "bg2";
                ViewBag.Title = "Введіть Email для пошуку";
                ViewBag.Prompt = "Email:";
            }
            // Передаємо повідомлення про помилку (якщо є)
            ViewBag.Error = TempData["Error"];
            return View();
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
