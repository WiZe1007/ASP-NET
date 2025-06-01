using System.ComponentModel.DataAnnotations;

namespace Task_5.Models
{
    public class TriangleViewModel
    {
        // Значення гіпотенузи (обов'язкове поле)
        [Required(ErrorMessage = "Будь ласка, введіть значення гіпотенузи.")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Гіпотенуза має бути додатнім числом.")]
        [Display(Name = "Гіпотенуза")]
        public double? Hypotenuse { get; set; }

        // Тип розрахунку: "Периметр", "Площа" або "Обидва"
        [Display(Name = "Розрахунок")]
        public string? CalculationType { get; set; }

        // Результат розрахунку периметру
        [Display(Name = "Периметр")]
        public double? Perimeter { get; set; }

        // Результат розрахунку площі
        [Display(Name = "Площа")]
        public double? Area { get; set; }

        // Повідомлення про помилку, якщо щось не так
        public string? ErrorMessage { get; set; }
    }
}
