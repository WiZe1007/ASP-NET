using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class EditPhoneViewModel
    {
        [Required, Phone]
        public string Phone { get; set; } = null!;
    }
}
