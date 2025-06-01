using System;
using System.ComponentModel.DataAnnotations;

namespace Task_5.Models
{
    public class VisitorViewModel
    {
        [Display(Name = "Ім'я")]
        public string? Name { get; set; }

        [Display(Name = "Телефон")]
        public string? Phone { get; set; }

        [Display(Name = "Пошта")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Дата відвідування")]
        [DataType(DataType.Date)]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Вік")]
        public string? Age { get; set; }

        [Display(Name = "Улюблена кухня")]
        public string? Cuisine { get; set; }

        [Display(Name = "Які страви ви б хотіли бачити в меню?")]
        public string? Dishes { get; set; }

        [Display(Name = "Чому обрали нашу установу?")]
        public string? Reason { get; set; }

        [Display(Name = "Чи будете рекомендувати нашу установу друзям та знайомим?")]
        public string? Recommend { get; set; }
    }
}
