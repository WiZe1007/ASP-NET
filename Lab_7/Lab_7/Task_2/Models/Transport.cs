namespace Task_2.Models
{
    public class Transport
    {
        public int Id { get; set; }
        public string VidTransportu { get; set; } = string.Empty; // Tr / Tl / A
        public string NomMarshruta { get; set; } = string.Empty; // ≤15 симв.
        public double ProtjazhnistMarshruta { get; set; }              // >0
        public int ChasVDorozi { get; set; }                 // ≥1
        public int KilkistZupynok { get; set; }                 // ≥0
    }
}
