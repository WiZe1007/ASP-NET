using System.ComponentModel.DataAnnotations;

namespace Task_6.Models
{
    public class EditEmailViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}
