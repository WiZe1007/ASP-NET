using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task_3.Models;

namespace Task_3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Зберігає кількість стовпців
        public DbSet<ColumnCount> ColumnCounts { get; set; }
    }
}
