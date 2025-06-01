using Microsoft.EntityFrameworkCore;
using Task_2.Models;

namespace Task_2.Data
{
    // Контекст бази даних EF Core
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts) { }  // Конструктор із параметрами конфігурації

        public DbSet<User> Users { get; set; } = null!;  // Набір користувачів у БД
    }
}
