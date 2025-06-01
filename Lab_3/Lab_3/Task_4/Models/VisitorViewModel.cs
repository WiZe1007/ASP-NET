using System;
using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class VisitorViewModel
    {
        // Властивість для імені відвідувача
        [Display(Name = "Ім'я")]
        public string? Name { get; set; }

        // Властивість для номеру телефону
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }

        // Властивість для електронної пошти з валідацією формату
        [Display(Name = "Пошта")]
        [EmailAddress]
        public string? Email { get; set; }

        // Властивість для дати відвідування (тип DateTime)
        [Display(Name = "Дата відвідування")]
        [DataType(DataType.Date)]
        public DateTime? VisitDate { get; set; }

        // Властивість для вибору віку
        [Display(Name = "Вік")]
        public string? Age { get; set; }

        // Властивість для улюбленої кухні
        [Display(Name = "Улюблена кухня")]
        public string? Cuisine { get; set; }

        // Властивість для побажань щодо страв, які має бачити меню
        [Display(Name = "Які страви ви б хотіли бачити в меню?")]
        public string? Dishes { get; set; }

        // Властивість для вибору причини відвідування
        [Display(Name = "Чому обрали нашу установу?")]
        public string? Reason { get; set; }

        // Властивість для відповіді, чи рекомендуватимуть ресторан
        [Display(Name = "Чи будете рекомендувати нашу установу друзям та знайомим?")]
        public string? Recommend { get; set; }
    }
}
