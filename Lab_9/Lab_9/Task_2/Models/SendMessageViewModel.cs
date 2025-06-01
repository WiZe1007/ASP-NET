using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class SendMessageViewModel
    {
        [Required(ErrorMessage = "Оберіть отримувача")]
        public string To { get; set; } = null!;

        [Required(ErrorMessage = "Введіть тему")]
        public string Theme { get; set; } = null!;

        [Required(ErrorMessage = "Введіть текст повідомлення")]
        public string Text { get; set; } = null!;
    }
}
