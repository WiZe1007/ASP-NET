using Microsoft.EntityFrameworkCore;
using Task_2.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) �������� ��
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) MVC �� ���
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
