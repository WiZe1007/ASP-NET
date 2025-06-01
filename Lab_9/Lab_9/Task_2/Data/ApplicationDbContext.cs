using Microsoft.EntityFrameworkCore;
using Task_2.Models;

namespace Task_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
