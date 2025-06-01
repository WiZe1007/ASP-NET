using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task_4.Models;

namespace Task_4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
        : base(opts) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
