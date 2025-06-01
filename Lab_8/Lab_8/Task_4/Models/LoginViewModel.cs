using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; } = null!;  // Валідація для поля Name

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;  // Валідація для поля Password
    }
}
