using Microsoft.EntityFrameworkCore;
using Task_8.Models;

namespace Task_8.Data
{
    public class AppDbContext : DbContext
    {
        // Конструктор, що приймає параметри для налаштування DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet для таблиці Employees
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Вказуємо точний SQL-тип для зарплати, щоб значення не обрізались
            modelBuilder.Entity<Employee>()
                .Property(e => e.e_salary)
                .HasColumnType("decimal(18,2)");
        }
    }
}
