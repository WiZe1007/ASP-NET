using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Task_6.Models
{
    public class TransportContext : DbContext
    {
        public TransportContext(DbContextOptions<TransportContext> options)
            : base(options)
        {
        }

        // DB набір для записів транспорту
        public DbSet<Transport> Transports { get; set; }
    }
}
