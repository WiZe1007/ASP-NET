using Microsoft.EntityFrameworkCore;
using Task_4.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) MVC + сесії + HttpContextAccessor
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
