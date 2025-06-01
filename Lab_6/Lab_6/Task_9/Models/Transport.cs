namespace Task_9.Models
{
    public class Transport
    {
        public int Id { get; set; }                     // Первинний ключ
        public string VidTransportu { get; set; }         // "Tr", "Tl", "A"
        public string NomMarshruta { get; set; }          // Номер маршруту (якщо є букви, тип string)
        public float ProtjazhnistMarshruta { get; set; }    // Протяжність (км)
        public int ChasVDorozi { get; set; }             // Час в дорозі (хв)
        public int KilkistZupynok { get; set; }          // Кількість зупинок
    }
}
