using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_5.Models
{
    public class TestAnswerViewModel
    {
        // приховані поля
        public int Id { get; set; }
        public char QuestionType { get; set; }
        public string CorrectAnswers { get; set; } = null!;

        // загальні для всіх
        public string Text { get; set; } = null!;

        // варіанти для R та C
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        // Ввід користувача:
        // для R
        public char? SelectedOption { get; set; }

        // для C
        public List<string> SelectedOptions { get; set; } = new List<string>();

        // для T
        public string? TextAnswer { get; set; }
    }
}
