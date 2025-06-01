using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class RegisterViewModel
    {
        [Required, StringLength(15), RegularExpression("^[A-Za-z0-9]+$")]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Phone]
        public string Phone { get; set; } = null!;

        [Required, MinLength(12), DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[!@#$%^&*()]).+$")]
        public string Password { get; set; } = null!;

        [Required, Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
