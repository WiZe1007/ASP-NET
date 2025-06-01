namespace Task_12.Models
{
    public class InterestsModel
    {
        // За замовчуванням увімкнено "Спорт" та "Майстрування"
        public bool Sport { get; set; } = true;
        public bool Travel { get; set; } = false;
        public bool Craft { get; set; } = true;
        public bool Draw { get; set; } = false;
    }
}
