using Microsoft.EntityFrameworkCore;
using Task_2.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// ����� ���������� � appsettings.json
string connectionString = builder.Configuration.GetConnectionString("TransportConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// �������� ������� ��� Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // UseHsts �� UseHttpsRedirection �� �������
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ������� �� �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
