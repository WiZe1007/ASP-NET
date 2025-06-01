using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ������ MVC-������
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // ������������ ������� ������� ��� Production
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // ������ �� ��������� ����� (css, js ����)
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            // ������� �� �������������: ����������� Transport/Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Transport}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
