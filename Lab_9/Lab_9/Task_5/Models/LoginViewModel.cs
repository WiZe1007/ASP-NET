using System.ComponentModel.DataAnnotations;

namespace Task_5.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
