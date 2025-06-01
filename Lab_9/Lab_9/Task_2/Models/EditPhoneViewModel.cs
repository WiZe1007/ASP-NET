using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class EditPhoneViewModel
    {
        [Required(ErrorMessage = "Введіть новий телефон")]
        [Phone(ErrorMessage = "Невірний телефон")]
        public string Phone { get; set; } = null!;
    }
}
