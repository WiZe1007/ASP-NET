-- Створення таблиці (якщо її ще немає):
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE [name] = 'ImageDimensions')
BEGIN
    CREATE TABLE [dbo].[ImageDimensions](
        [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        [Width] NVARCHAR(50) NOT NULL,
        [Height] NVARCHAR(50) NOT NULL
    );
END

-- Вставлення кількох записів:
INSERT INTO [dbo].[ImageDimensions] (Width, Height)
VALUES
('80px','60px'),
('100px','70px'),
('120px','120px'),
('140px','150px'),
('160px','220px');
