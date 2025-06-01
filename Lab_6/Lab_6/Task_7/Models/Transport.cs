namespace Task_7.Models
{
    public class Transport
    {
        public int Id { get; set; }                     // Первинний ключ
        public string VidTransportu { get; set; }         // Вид транспорту (наприклад, "Tr", "Tl", "A")
        public int NomMarshruta { get; set; }             // Номер маршруту
        public float ProtjazhnistMarshruta { get; set; }   // Протяжність маршруту (км)
        public int ChasVDorozi { get; set; }              // Час в дорозі (хв)
    }
}
