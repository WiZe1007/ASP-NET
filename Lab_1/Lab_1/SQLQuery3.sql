-- Створення таблиці ImageDimensions (якщо її ще немає)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ImageDimensions')
BEGIN
    CREATE TABLE [dbo].[ImageDimensions](
        [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        [Width] NVARCHAR(50) NOT NULL,
        [Height] NVARCHAR(50) NOT NULL
    );
END

-- Вставлення 3 записів для нашої топології (зобр1, зобр2, зобр3):
INSERT INTO [dbo].[ImageDimensions] (Width, Height)
VALUES
('60px', '120px'),   -- Зобр1: вертикально-витягнутий
('300px', '100px'),  -- Зобр2: широкий
('150px', '50px');   -- Зобр3: горизонтальний прямокутник
