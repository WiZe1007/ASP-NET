// Program.cs
using Task_6.Services;
using Task_6.Models;

var builder = WebApplication.CreateBuilder(args);

// FileStore для користувачів
builder.Services.AddSingleton<FileStore<User>>(sp =>
    new FileStore<User>(sp.GetRequiredService<IWebHostEnvironment>(), "users.json"));

// Сервіс налаштувань (ліміт спроб)
builder.Services.AddSingleton<SettingsService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Splash}/{action=Index}/{id?}"
);

app.Run();
