namespace Task_2.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public int Score { get; set; }
        public DateTime TakenAt { get; set; }
    }
}
