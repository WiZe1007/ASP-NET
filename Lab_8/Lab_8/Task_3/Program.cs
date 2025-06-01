using Microsoft.EntityFrameworkCore;
using Task_3.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext для LocalDB
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC + сесії та HttpContextAccessor
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();
