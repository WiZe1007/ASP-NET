using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_4.Models
{
    public class Transport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вид транспорту (Tr, Tl, A) є обов'язковим!")]
        [RegularExpression("^(Tr|Tl|A)$",
            ErrorMessage = "Допустимі лише Tr, Tl або A.")]
        public string VidTransportu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер маршруту обов'язковий!")]
        [StringLength(15, ErrorMessage = "Максимум 15 символів.")]
        public string NomMarshruta { get; set; } = string.Empty;

        [Range(0.01, 1_000_000,
            ErrorMessage = "Протяжність має бути > 0 км.")]
        [Column(TypeName = "float")]           // double у SQL Server
        public double ProtjazhnistMarshruta { get; set; }

        [Range(1, 100_000,
            ErrorMessage = "Час у дорозі має бути ≥ 1 хв.")]
        public int ChasVDorozi { get; set; }

        [Range(0, 10_000,
            ErrorMessage = "К‑сть зупинок не може бути від’ємною!")]
        public int KilkistZupynok { get; set; }
    }
}
