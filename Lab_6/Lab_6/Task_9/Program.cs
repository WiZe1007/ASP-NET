using Microsoft.EntityFrameworkCore;
using Task_9.Data;
using Task_9.Models;

var builder = WebApplication.CreateBuilder(args);

// ������ MVC
builder.Services.AddControllersWithViews();

// ����������� DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// �������� ������� �� ����� ��� �����
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // ����������� �� ��������� �������
    dbContext.Database.Migrate();

    // ������ ������, ���� ������ (Tr, Tl, A)
    if (!dbContext.Transports.Any(x => x.VidTransportu == "Tr" && x.NomMarshruta == "12"))
    {
        dbContext.Transports.Add(new Transport
        {
            VidTransportu = "Tr",
            NomMarshruta = "12",
            ProtjazhnistMarshruta = 27.55f,
            ChasVDorozi = 75,
            KilkistZupynok = 7    // ���������, 7 �������
        });
    }
    if (!dbContext.Transports.Any(x => x.VidTransportu == "Tl" && x.NomMarshruta == "17"))
    {
        dbContext.Transports.Add(new Transport
        {
            VidTransportu = "Tl",
            NomMarshruta = "17",
            ProtjazhnistMarshruta = 13.6f,
            ChasVDorozi = 57,
            KilkistZupynok = 5    // 5 �������
        });
    }
    if (!dbContext.Transports.Any(x => x.VidTransportu == "A" && x.NomMarshruta == "12a"))
    {
        dbContext.Transports.Add(new Transport
        {
            VidTransportu = "A",
            NomMarshruta = "12a",
            ProtjazhnistMarshruta = 57.3f,
            ChasVDorozi = 117,
            KilkistZupynok = 10   // 10 �������
        });
    }
    dbContext.SaveChanges();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transport}/{action=Index}/{id?}");

app.Run();
