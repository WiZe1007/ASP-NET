using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class EditNameViewModel
    {
        [Required, StringLength(15), RegularExpression("^[A-Za-z0-9]+$")]
        public string Name { get; set; } = null!;
    }
}
