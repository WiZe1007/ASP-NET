using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Додаємо MVC-сервіси
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Налаштування обробки помилок для Production
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Доступ до статичних файлів (css, js тощо)
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            // Маршрут за замовчуванням: відкриватиме Transport/Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Transport}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
