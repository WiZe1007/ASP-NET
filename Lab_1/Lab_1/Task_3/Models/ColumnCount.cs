using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class ColumnCount
    {
        [Key]
        public int Id { get; set; }

        // Кількість стовпчиків
        public int Columns { get; set; }
    }
}
