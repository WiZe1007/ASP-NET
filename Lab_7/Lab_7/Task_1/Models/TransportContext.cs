using Microsoft.EntityFrameworkCore;

namespace Task_1.Models
{
    public class TransportContext : DbContext
    {
        public TransportContext(DbContextOptions<TransportContext> options)
            : base(options) { }

        public DbSet<Transport> Transports => Set<Transport>();
    }
}
