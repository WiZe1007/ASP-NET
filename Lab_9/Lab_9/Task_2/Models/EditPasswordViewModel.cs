using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class EditPasswordViewModel
    {
        [Required(ErrorMessage = "Введіть новий пароль")]
        [MinLength(12, ErrorMessage = "Мінімум 12 символів")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()]).+$", ErrorMessage = "Потрібен спецсимвол")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("NewPassword", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
