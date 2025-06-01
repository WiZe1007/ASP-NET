using Task_6.Services;
using Task_6.Models;

var builder = WebApplication.CreateBuilder(args);

// File-based stores
builder.Services.AddSingleton<FileStore<User>>(sp =>
    new FileStore<User>(sp.GetRequiredService<IWebHostEnvironment>(), "users.json"));
builder.Services.AddSingleton<FileStore<Message>>(sp =>
    new FileStore<Message>(sp.GetRequiredService<IWebHostEnvironment>(), "messages.json"));

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
