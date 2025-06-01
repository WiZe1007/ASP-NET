using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class EditEmailViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}
