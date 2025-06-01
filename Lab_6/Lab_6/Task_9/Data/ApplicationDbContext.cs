using Microsoft.EntityFrameworkCore;
using Task_9.Models;

namespace Task_9.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Зберігання маршрутів
        public DbSet<Transport> Transports { get; set; }
    }
}
