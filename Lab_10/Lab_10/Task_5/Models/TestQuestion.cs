namespace Task_5.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        /// <summary>
        /// 'R' = radio (1 варіант), 'C' = checkbox (декілька), 'T' = text (вільний ввід)
        /// </summary>
        public char QuestionType { get; set; }

        /// <summary>
        /// Для R: "A"; C: "AC"; T: "правильний текст"
        /// </summary>
        public string CorrectAnswers { get; set; } = null!;
    }
}
