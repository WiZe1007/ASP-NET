using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class EditPhoneViewModel
    {
        [Required, Phone]
        public string Phone { get; set; } = null!;
    }
}
