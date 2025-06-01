using Microsoft.EntityFrameworkCore;

namespace Task_4.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Transport> Transports => Set<Transport>();
    }
}
