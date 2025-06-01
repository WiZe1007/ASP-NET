using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class EditEmailViewModel
    {
        [Required(ErrorMessage = "Введіть новий Email")]
        [EmailAddress(ErrorMessage = "Невірний Email")]
        public string Email { get; set; } = null!;
    }
}
