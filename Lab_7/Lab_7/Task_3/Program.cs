using Microsoft.EntityFrameworkCore;
using Task_3.Models;

var builder = WebApplication.CreateBuilder(args);

// Додаємо MVC (контролери з представленнями)
builder.Services.AddControllersWithViews();

// Рядок підключення з appsettings.json
string connectionString = builder.Configuration.GetConnectionString("TransportConnection");

// Підключаємо SQL Server (LocalDB)
builder.Services.AddDbContext<TransportContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Якщо не Development
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Можливе додавання UseHsts, UseHttpsRedirection тощо
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Маршрут за замовчуванням => controller=Transport, action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
