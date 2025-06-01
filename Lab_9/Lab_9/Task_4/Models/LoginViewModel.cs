using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть ім’я")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
