using System.ComponentModel.DataAnnotations;

namespace Task_7.Models
{
    public class StringProcessingViewModel
    {
        [Display(Name = "Введіть рядок (не менше 5 слів)")]
        public string? InputText { get; set; } // Текст, який вводить користувач

        [Display(Name = "Кількість повторень")]
        public int? RepeatCount { get; set; } // Кількість повторень виводу рядка
    }
}
