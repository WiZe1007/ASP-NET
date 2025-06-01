using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class TestAnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        // Приберемо [Required], щоб не було model‐state помилки
        public char? SelectedOption { get; set; }

        // цільова правильна відповідь з БД
        public char CorrectOption { get; set; }
    }
}
