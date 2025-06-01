using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class EditEmailViewModel
    {
        [Required(ErrorMessage = "Введіть новий Email")]
        [EmailAddress(ErrorMessage = "Невірний формат Email")]
        public string Email { get; set; } = null!;
    }
}
