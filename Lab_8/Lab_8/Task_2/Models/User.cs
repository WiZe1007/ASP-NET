namespace Task_2.Models
{
    // Сутність користувача (таблиця Users у БД)
    public class User
    {
        public int Id { get; set; }           // Унікальний ідентифікатор
        public string Name { get; set; } = null!;     // Ім’я користувача
        public string Email { get; set; } = null!;    // Email адреса
        public string Phone { get; set; } = null!;    // Номер телефону
        public string Password { get; set; } = null!; // Пароль (зберігається в чистому вигляді для прикладу)
    }
}
