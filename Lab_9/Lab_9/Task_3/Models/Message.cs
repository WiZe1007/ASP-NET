using System;

namespace Task_3.Models
{
    public class Message
    {
        public Guid Id { get; set; }         // Унікальний ідентифікатор
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string Theme { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}
