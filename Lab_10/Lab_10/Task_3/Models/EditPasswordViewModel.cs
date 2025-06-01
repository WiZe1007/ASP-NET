using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class EditPasswordViewModel
    {
        [Required, MinLength(12), DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[!@#$%^&*()]).+$")]
        public string NewPassword { get; set; } = null!;

        [Required, Compare("NewPassword"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
