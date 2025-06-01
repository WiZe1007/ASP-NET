namespace Task_5.Models
{
    public class Transport
    {
        // ID (генерується автоматично)
        public int Id { get; set; }

        // Вид транспорту (наприклад, Tr, Tl, A)
        public string TransportType { get; set; }

        // Номер маршруту, напр. "12a"
        public string RouteNumber { get; set; }

        // Протяжність маршруту (км)
        public double RouteDistance { get; set; }

        // Час в дорозі (хв)
        public int TravelTime { get; set; }
    }
}
