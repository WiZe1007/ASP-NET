using Microsoft.EntityFrameworkCore;
using Task_3.Models;

var builder = WebApplication.CreateBuilder(args);

// ������ MVC (���������� � ���������������)
builder.Services.AddControllersWithViews();

// ����� ���������� � appsettings.json
string connectionString = builder.Configuration.GetConnectionString("TransportConnection");

// ϳ�������� SQL Server (LocalDB)
builder.Services.AddDbContext<TransportContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// ���� �� Development
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // ������� ��������� UseHsts, UseHttpsRedirection ����
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ������� �� ������������� => controller=Transport, action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
