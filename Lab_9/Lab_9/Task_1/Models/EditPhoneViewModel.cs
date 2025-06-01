using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class EditPhoneViewModel
    {
        [Required(ErrorMessage = "Введіть новий телефон")]
        [Phone(ErrorMessage = "Невірний формат телефону")]
        public string Phone { get; set; } = null!;
    }
}
