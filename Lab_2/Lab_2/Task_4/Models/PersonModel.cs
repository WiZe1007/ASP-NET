namespace Task_4.Models
{
    public class PersonModel
    {
        // Властивість для імені; за замовчуванням – порожній рядок
        public string Name { get; set; } = "";
        // Властивість для номера телефону
        public string Phone { get; set; } = "";
        // Властивість для електронної пошти
        public string Email { get; set; } = "";
        // Властивість для дати народження (як рядок)
        public string Birthdate { get; set; } = "";
    }
}
