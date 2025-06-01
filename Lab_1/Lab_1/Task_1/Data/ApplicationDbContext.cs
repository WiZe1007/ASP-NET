using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task_1.Models;

namespace Task_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Описуємо набір (таблицю) ParagraphStyles:
        public DbSet<ParagraphStyle> ParagraphStyles { get; set; }
    }
}
