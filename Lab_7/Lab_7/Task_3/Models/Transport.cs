using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    public class Transport
    {
        public int Id { get; set; }               // PK, генерується БД

        [Required(ErrorMessage = "Вид транспорту (Tr, Tl, A) є обов'язковим!")]
        [RegularExpression("^(Tr|Tl|A)$",
            ErrorMessage = "Допустимі значення тільки: Tr, Tl, A.")]
        public string TransportType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер маршруту обов'язковий!")]
        [StringLength(10, ErrorMessage = "Максимум 10 символів.")]
        public string RouteNumber { get; set; } = string.Empty;

        [Range(0.01, 100_000,
            ErrorMessage = "Протяжність має бути більше 0 км.")]
        public double RouteDistance { get; set; }

        [Range(1, 100_000,
            ErrorMessage = "Час у дорозі має бути не менше 1 хв.")]
        public int TravelTime { get; set; }
    }
}
