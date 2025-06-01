using Microsoft.EntityFrameworkCore;
using Task_7.Models;

namespace Task_7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Набір даних для транспорту
        public DbSet<Transport> Transports { get; set; }
    }
}
