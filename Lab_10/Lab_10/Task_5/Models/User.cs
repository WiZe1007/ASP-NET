// Models/User.cs
namespace Task_5.Models
{
    public class User
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        // ← НОВЕ поле для збереження останньої оцінки 0–100
        public int Score { get; set; }
    }
}
