using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_8.Models
{
    [Table("Employees")] // Вказуємо, що ця модель відповідає таблиці "Employees" в БД
    public class Employee
    {
        [Key] // Це первинний ключ
        public int e_id { get; set; }

        [Required] // Обов'язкове поле, не може бути пустим
        public string e_name { get; set; } = string.Empty;

        // Зарплата співробітника
        public decimal e_salary { get; set; }

        // Вік співробітника
        public int e_age { get; set; }

        // Стать співробітника
        public string e_gender { get; set; } = string.Empty;

        // Назва відділу, де працює співробітник
        public string e_dept { get; set; } = string.Empty;
    }
}
