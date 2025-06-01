using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Task_4.Models
{
    public class TransportContext : DbContext
    {
        public TransportContext(DbContextOptions<TransportContext> options)
            : base(options)
        {
        }

        // Таблиця для зберігання записів транспорту
        public DbSet<Transport> Transports { get; set; }
    }
}
