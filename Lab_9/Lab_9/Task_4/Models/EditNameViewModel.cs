using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class EditNameViewModel
    {
        [Required(ErrorMessage = "Введіть нове ім’я")]
        [StringLength(15, ErrorMessage = "Максимум 15 символів")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Лише латиниця та цифри")]
        public string Name { get; set; } = null!;
    }
}
