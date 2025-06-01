namespace Task_4.Models
{
    public class Transport
    {
        // ID запису (генерується в базі)
        public int Id { get; set; }

        // Вид транспорту (Tr, Tl, A)
        public string TransportType { get; set; }

        // Номер маршруту
        public string RouteNumber { get; set; }

        // Протяжність маршруту (км)
        public double RouteDistance { get; set; }

        // Час в дорозі (хв)
        public int TravelTime { get; set; }
    }
}
