using Microsoft.EntityFrameworkCore;
using Task_4.Models;

namespace Task_4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts) { }  // Конструктор з параметрами

        public DbSet<User> Users { get; set; } = null!;  // Таблиця користувачів у БД
    }
}
