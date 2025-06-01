// Models/User.cs
namespace Task_6.Models
{
    public class User
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Остання тестова оцінка
        public int Score { get; set; }

        // Роль: "User" або "Master"
        public string Role { get; set; } = "User";
    }
}
