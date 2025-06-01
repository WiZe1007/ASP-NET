using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Task_4.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            // Зберігаємо конфігурацію для роботи з рядком підключення
            _configuration = configuration;
        }

        // Step 1: Введення пароля
        [HttpGet]
        public IActionResult Step1()
        {
            // Просто повертаємо форму вводу пароля
            return View();
        }

        [HttpPost]
        public IActionResult Step1(string password)
        {
            // Якщо пароль не введено – повертаємо форму з помилкою
            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Будь ласка, введіть пароль.");
                return View();
            }
            // Зберігаємо пароль у сесії
            HttpContext.Session.SetString("Password", password);
            return RedirectToAction("Step2");
        }

        // Step 2: Введення Email
        [HttpGet]
        public IActionResult Step2()
        {
            // Якщо пароль не заданий, перенаправляємо назад до Step1
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Password")))
                return RedirectToAction("Step1");
            return View();
        }

        [HttpPost]
        public IActionResult Step2(string email)
        {
            // Перевіряємо, чи Email не пустий
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Будь ласка, введіть Email.");
                return View();
            }
            // Зберігаємо Email у сесії
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("Step3");
        }

        // Step 3: Введення адреси
        [HttpGet]
        public IActionResult Step3()
        {
            // Якщо пароль або Email не задані, повертаємося на початок
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Password")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToAction("Step1");
            return View();
        }

        [HttpPost]
        public IActionResult Step3(string address)
        {
            // Перевірка на заповнення адреси
            if (string.IsNullOrWhiteSpace(address))
            {
                ModelState.AddModelError("", "Будь ласка, введіть адресу.");
                return View();
            }
            // Зберігаємо адресу у сесії
            HttpContext.Session.SetString("Address", address);
            return RedirectToAction("Step4");
        }

        // Step 4: Введення логіну та запис до бази даних
        [HttpGet]
        public IActionResult Step4()
        {
            // Якщо даних не вистачає – повертаємо користувача до початку
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Password")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("Email")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("Address")))
                return RedirectToAction("Step1");

            return View();
        }

        [HttpPost]
        public IActionResult Step4(string login)
        {
            // Перевірка, чи логін введено
            if (string.IsNullOrWhiteSpace(login))
            {
                ModelState.AddModelError("", "Будь ласка, введіть логін.");
                return View();
            }
            // Зберігаємо логін у сесії
            HttpContext.Session.SetString("Login", login);

            // Збираємо всі дані для запису в базу
            string password = HttpContext.Session.GetString("Password");
            string email = HttpContext.Session.GetString("Email");
            string address = HttpContext.Session.GetString("Address");
            string loginVal = HttpContext.Session.GetString("Login");

            // Рядок підключення до БД
            string connStr = _configuration.GetConnectionString("RegistrationDb");
            try
            {
                // Відкриваємо з'єднання і записуємо дані в таблицю Accounts
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "INSERT INTO Accounts (Password, Email, Address, Login) VALUES (@Password, @Email, @Address, @Login)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Login", loginVal);
                        cmd.ExecuteNonQuery();
                    }
                }
                // Якщо запис успішний – зберігаємо повідомлення про успіх
                TempData["Success"] = "Реєстрацію успішно завершено!";
            }
            catch (Exception ex)
            {
                // Якщо сталася помилка – записуємо повідомлення про помилку і залишаємо користувача на цій же сторінці
                TempData["Error"] = "Сталася помилка при записі даних: " + ex.Message;
                return RedirectToAction("Step4");
            }
            // Очищаємо сесію після завершення реєстрації
            HttpContext.Session.Clear();
            return RedirectToAction("Success");
        }

        // Success: Повідомлення про успішну реєстрацію
        [HttpGet]
        public IActionResult Success()
        {
            // Показуємо сторінку успіху
            return View();
        }
    }
}
