namespace Task_8.Models
{
    public class Transport
    {
        public int Id { get; set; }                     // Первинний ключ
        public string VidTransportu { get; set; }         // "Tr", "Tl", "A"
        public string NomMarshruta { get; set; }          // Номер маршруту (якщо є букви, це string)
        public float ProtjazhnistMarshruta { get; set; }    // Протяжність маршруту (км)
        public int ChasVDorozi { get; set; }             // Час в дорозі (хв)
    }
}
