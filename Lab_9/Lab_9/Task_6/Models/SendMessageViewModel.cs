using System.ComponentModel.DataAnnotations;

namespace Task_6.Models
{
    public class SendMessageViewModel
    {
        [Required]
        public string To { get; set; } = null!;

        [Required]
        public string Theme { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;
    }
}
