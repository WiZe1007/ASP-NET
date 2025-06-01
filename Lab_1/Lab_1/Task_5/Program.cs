using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// **Додаємо** підтримку Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Можна задати час життя сесії, інші параметри
    // options.IdleTimeout = TimeSpan.FromMinutes(20);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// **Увімкнути** сесію
app.UseSession();

// Змінюємо, щоб за замовчуванням відкривалася Стор1
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Page1}/{action=Index}/{id?}");

app.Run();
