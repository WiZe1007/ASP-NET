namespace Task_3.Models
{
    public class User
    {
        public int Id { get; set; }  // Унікальний ідентифікатор
        public string Name { get; set; } = null!;  // Ім’я користувача
        public string Email { get; set; } = null!;  // Email користувача
        public string Phone { get; set; } = null!;  // Номер телефону користувача
        public string Password { get; set; } = null!;  // Пароль користувача
    }
}
