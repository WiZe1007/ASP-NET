using Microsoft.EntityFrameworkCore;
using Task_8.Models;

namespace Task_8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DB набір для зберігання маршрутів
        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Початкові дані для демонстрації
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 1,
                    VidTransportu = "Tr",
                    NomMarshruta = "12",
                    ProtjazhnistMarshruta = 27.55f,
                    ChasVDorozi = 75
                },
                new Transport
                {
                    Id = 2,
                    VidTransportu = "Tl",
                    NomMarshruta = "17",
                    ProtjazhnistMarshruta = 13.6f,
                    ChasVDorozi = 57
                },
                new Transport
                {
                    Id = 3,
                    VidTransportu = "A",
                    NomMarshruta = "12a",
                    ProtjazhnistMarshruta = 57.3f,
                    ChasVDorozi = 117
                }
            );
        }
    }
}
