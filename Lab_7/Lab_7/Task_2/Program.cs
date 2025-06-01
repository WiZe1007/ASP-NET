using Microsoft.EntityFrameworkCore;
using Task_2.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Рядок підключення з appsettings.json
string connectionString = builder.Configuration.GetConnectionString("TransportConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Обробник помилок для Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // UseHsts чи UseHttpsRedirection за потреби
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Маршрут за замовчуванням
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
