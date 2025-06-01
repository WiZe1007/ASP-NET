using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task_2_1.Models;

namespace Task_2_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ImageDimension> ImageDimensions { get; set; }
    }
}
