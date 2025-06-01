using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class ParagraphStyle
    {
        [Key]
        public int Id { get; set; }

        // Наприклад, розмір шрифту
        public string FontSize { get; set; } = string.Empty;

        // Відступи ззовні (margin)
        public string Margin { get; set; } = string.Empty;

        // Відступи зсередини (padding)
        public string Padding { get; set; } = string.Empty;
    }
}
