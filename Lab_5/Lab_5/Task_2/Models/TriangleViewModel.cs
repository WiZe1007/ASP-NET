namespace Task_2.Models
{
    public class TriangleViewModel
    {
        // Крок введення:
        // 0 – введення сторони A,
        // 1 – введення сторони B,
        // 2 – введення сторони C,
        // 3 – показ результату
        public int Step { get; set; }

        // Зберігаємо введене значення сторони A
        public string SideA { get; set; }
        // Зберігаємо введене значення сторони B
        public string SideB { get; set; }
        // Зберігаємо введене значення сторони C
        public string SideC { get; set; }

        // Результат обчислення (повідомлення для користувача)
        public string Result { get; set; }
    }
}
