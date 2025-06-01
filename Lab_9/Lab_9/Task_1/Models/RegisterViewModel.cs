using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть ім’я")]
        [StringLength(15, ErrorMessage = "Максимум 15 символів")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Лише латиниця та цифри")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress(ErrorMessage = "Невірний формат Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Введіть телефон")]
        [Phone(ErrorMessage = "Невірний формат телефону")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Введіть пароль")]
        [MinLength(12, ErrorMessage = "Мінімум 12 символів")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()]).+$", ErrorMessage = "Потрібен спецсимвол")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
