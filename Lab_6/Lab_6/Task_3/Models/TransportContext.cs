using Microsoft.EntityFrameworkCore;

namespace Task_3.Models
{
    public class TransportContext : DbContext
    {
        public TransportContext(DbContextOptions<TransportContext> options)
            : base(options)
        {
        }

        // Таблиця з транспортними записами
        public DbSet<Transport> Transports { get; set; }
    }
}
