using System;

namespace Task_2.Models
{
    public class Transport
    {
        // Ідентифікатор
        public int Id { get; set; }

        // Вид транспорту (Tr - трамвай, Tl - тролейбус, A - автобус)
        public string TransportType { get; set; }

        // Номер маршруту
        public string RouteNumber { get; set; }

        // Протяжність маршруту, км
        public double RouteDistance { get; set; }

        // Час в дорозі, хв
        public int TravelTime { get; set; }
    }
}
