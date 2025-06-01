using System.ComponentModel.DataAnnotations;

namespace Task_2_1.Models
{
    public class ImageDimension
    {
        [Key]
        public int Id { get; set; }

        // Ширина зображення (наприклад, "100px", "150px")
        public string Width { get; set; } = string.Empty;

        // Висота зображення (наприклад, "100px", "150px")
        public string Height { get; set; } = string.Empty;
    }
}
