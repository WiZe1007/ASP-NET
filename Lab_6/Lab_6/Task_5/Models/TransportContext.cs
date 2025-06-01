using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Task_5.Models
{
    public class TransportContext : DbContext
    {
        public TransportContext(DbContextOptions<TransportContext> options)
            : base(options)
        {
        }

        // Таблиця для записів транспорту
        public DbSet<Transport> Transports { get; set; }
    }
}
