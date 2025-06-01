using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter Name")]
        [StringLength(15, ErrorMessage = "Max 15 chars")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Only Latin & digits")]
        public string Name { get; set; } = null!;  // Валідація для поля Name

        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;  // Валідація для поля Email

        [Required(ErrorMessage = "Enter Phone")]
        [Phone(ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; } = null!;  // Валідація для поля Phone

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(12, ErrorMessage = "At least 12 chars")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()]).+$", ErrorMessage = "Need special char")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;  // Валідація для поля Password

        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;  // Валідація для поля ConfirmPassword
    }
}
