using Microsoft.EntityFrameworkCore;
using Task_7.Data;

var builder = WebApplication.CreateBuilder(args);

// Додаємо MVC-сервіси
builder.Services.AddControllersWithViews();

// Реєструємо контекст бази даних із використанням рядка підключення з appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
