using Microsoft.EntityFrameworkCore;
using Task_1.Models; // namespace вашої моделі

var builder = WebApplication.CreateBuilder(args);

// Додаємо контролери з представленнями (MVC)
builder.Services.AddControllersWithViews();

// Підключення до LocalDB з appsettings.json
string connectionString = builder.Configuration.GetConnectionString("TransportConnection");
builder.Services.AddDbContext<TransportContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Обробка помилок (якщо не режим розробки)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Можна додати UseHsts, UseHttpsRedirection тощо
}

// Використовуємо статичні файли (wwwroot)
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Основний маршрут
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
