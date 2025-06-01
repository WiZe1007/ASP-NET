using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class EditEmailViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}
