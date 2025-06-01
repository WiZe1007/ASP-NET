using System;

namespace Task_1.Models
{
    public class Transport
    {
        // Ідентифікатор запису
        public int Id { get; set; }

        // Вид транспорту (Tr - трамвай, Tl - тролейбус, A - автобус)
        public string TransportType { get; set; }

        // Номер маршруту (наприклад, "12a")
        public string RouteNumber { get; set; }

        // Протяжність маршруту в кілометрах
        public double RouteDistance { get; set; }

        // Час в дорозі (хвилини)
        public int TravelTime { get; set; }
    }
}
