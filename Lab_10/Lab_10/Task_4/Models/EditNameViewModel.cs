using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class EditNameViewModel
    {
        [Required, StringLength(15), RegularExpression("^[A-Za-z0-9]+$")]
        public string Name { get; set; } = null!;
    }
}
