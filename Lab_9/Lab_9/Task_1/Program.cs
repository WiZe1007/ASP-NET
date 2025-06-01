using Microsoft.EntityFrameworkCore;
using Task_1.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) Додаємо DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Залишаємо MVC, сесії та HttpContextAccessor
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}"
);

app.Run();
