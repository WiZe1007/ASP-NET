﻿namespace Task_1.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public char CorrectOption { get; set; }
    }
}
