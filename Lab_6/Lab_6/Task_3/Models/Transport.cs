namespace Task_3.Models
{
    public class Transport
    {
        // ID запису (генерується автоматично в БД)
        public int Id { get; set; }

        // Вид транспорту (Tr, Tl, A)
        public string TransportType { get; set; }

        // Номер маршруту, напр. "12a"
        public string RouteNumber { get; set; }

        // Протяжність маршруту в км
        public double RouteDistance { get; set; }

        // Час в дорозі в хвилинах
        public int TravelTime { get; set; }
    }
}
