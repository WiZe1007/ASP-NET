using Microsoft.EntityFrameworkCore;

namespace Task_2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Transport> Transports => Set<Transport>();
    }
}
