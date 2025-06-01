using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task_3.Models;

namespace Task_3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
        : base(opts) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
