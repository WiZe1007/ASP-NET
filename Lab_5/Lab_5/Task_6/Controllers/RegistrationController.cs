using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Task_6.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            // Зберігаємо конфігурацію для подальшого доступу до рядка підключення до БД
            _configuration = configuration;
        }

        // Єдиний метод, що обробляє як GET, так і POST запити для реєстрації
        public IActionResult Index(string input, bool reset = false)
        {
            // Якщо користувач хоче почати з початку, очищаємо сесію та TempData
            if (reset)
            {
                HttpContext.Session.Clear();
                TempData["Error"] = null;
                TempData["Success"] = null;
                return RedirectToAction("Index");
            }

            // Якщо запит має метод POST, обробляємо введені дані за етапами
            if (Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                // Етап 1: введення пароля
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Password")))
                {
                    HttpContext.Session.SetString("Password", input);
                }
                // Етап 2: введення Email
                else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                {
                    HttpContext.Session.SetString("Email", input);
                }
                // Етап 3: введення адреси
                else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Address")))
                {
                    HttpContext.Session.SetString("Address", input);
                }
                // Етап 4: введення логіну та запис до БД
                else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
                {
                    HttpContext.Session.SetString("Login", input);

                    // Отримуємо всі збережені дані з сесії
                    string password = HttpContext.Session.GetString("Password");
                    string email = HttpContext.Session.GetString("Email");
                    string address = HttpContext.Session.GetString("Address");
                    string login = HttpContext.Session.GetString("Login");

                    // Отримуємо рядок підключення до бази даних з конфігурації
                    string connStr = _configuration.GetConnectionString("RegistrationDb");
                    try
                    {
                        // Підключаємось до бази і виконуємо вставку даних у таблицю Accounts
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            string sql = "INSERT INTO Accounts (Password, Email, Address, Login) VALUES (@Password, @Email, @Address, @Login)";
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@Password", password);
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@Address", address);
                                cmd.Parameters.AddWithValue("@Login", login);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        // Якщо запис пройшов успішно, зберігаємо повідомлення для користувача
                        TempData["Success"] = "Реєстрацію успішно завершено!";
                    }
                    catch (Exception ex)
                    {
                        // У випадку помилки зберігаємо текст помилки в TempData
                        TempData["Error"] = "Сталася помилка при записі даних: " + ex.Message;
                    }
                    // Очищаємо сесію, щоб уникнути повторного сабміту
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index");
                }
                // Після обробки даних виконуємо редірект для запобігання повторного сабміту форми
                return RedirectToAction("Index");
            }

            // Якщо запит GET – визначаємо поточний етап заповнення форми за даними в сесії
            int step = 1;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Password")))
                step = 1;
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                step = 2;
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Address")))
                step = 3;
            else if (string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
                step = 4;
            ViewBag.Step = step;

            // Призначаємо відповідний CSS-клас для фону залежно від етапу
            switch (step)
            {
                case 1: ViewBag.BodyClass = "bg1"; break;
                case 2: ViewBag.BodyClass = "bg2"; break;
                case 3: ViewBag.BodyClass = "bg3"; break;
                case 4: ViewBag.BodyClass = "bg4"; break;
            }

            // Встановлюємо заголовок і підказку для форми згідно з поточним етапом
            string title = "";
            string prompt = "";
            switch (step)
            {
                case 1:
                    title = "Введіть пароль";
                    prompt = "Пароль:";
                    break;
                case 2:
                    title = "Введіть Email";
                    prompt = "Email:";
                    break;
                case 3:
                    title = "Введіть адресу";
                    prompt = "Адреса:";
                    break;
                case 4:
                    title = "Введіть логін";
                    prompt = "Логін:";
                    break;
            }
            // Передаємо дані у View через ViewBag
            ViewBag.Title = title;
            ViewBag.Prompt = prompt;
            ViewBag.Error = TempData["Error"];
            ViewBag.Success = TempData["Success"];

            return View();
        }
    }
}
