-- Приклад скрипту створення таблиці (якщо б міграція не використовувалась):

CREATE TABLE [dbo].[ParagraphStyles](
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [FontSize] NVARCHAR(50) NOT NULL,
    [Margin] NVARCHAR(50) NOT NULL,
    [Padding] NVARCHAR(50) NOT NULL
);

-- Наповнення тестовими даними:
INSERT INTO [dbo].[ParagraphStyles] ([FontSize],[Margin],[Padding])
VALUES 
('14px','10px','5px'),   -- Абзац 1
('18px','15px','10px'),  -- Абзац 2
('22px','20px','15px');  -- Абзац 3
