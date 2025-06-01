namespace Task_1.Models
{
    // POCO‑клас без атрибутів
    public class Transport
    {
        public int Id { get; set; }
        public string TransportType { get; set; } = string.Empty; // Tr / Tl / A
        public string RouteNumber { get; set; } = string.Empty; // до 10 симв.
        public double RouteDistance { get; set; }                // км > 0
        public int TravelTime { get; set; }                // хв >= 1
    }
}
