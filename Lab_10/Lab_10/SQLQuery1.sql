GO

USE TestingDB;
GO

-- Таблиця запитань для тесту
CREATE TABLE Questions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Text NVARCHAR(500) NOT NULL,
    OptionA NVARCHAR(200) NOT NULL,
    OptionB NVARCHAR(200) NOT NULL,
    OptionC NVARCHAR(200) NOT NULL,
    OptionD NVARCHAR(200) NOT NULL,
    CorrectOption CHAR(1) NOT NULL
);
GO

-- Таблиця результатів тестування
CREATE TABLE TestResults (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Score INT NOT NULL,
    TakenAt DATETIME NOT NULL
);
GO

-- Прикладові запитання з Історії України
INSERT INTO Questions (Text, OptionA, OptionB, OptionC, OptionD, CorrectOption) VALUES
('Яка дата Першого Збору Української Центральної Ради?', 
 '17 березня 1917', '7 березня 1917', '17 квітня 1917', '7 квітня 1917', 'A'),
('Хто очолив УНР у 1918 році як гетьман?', 
 'Володимир Винниченко', 'Олександр Шульгин', 'Павло Скоропадський', 'Симон Петлюра', 'C'),
('Коли відбулося проголошення Акта Злуки УНР та ЗУНР?', 
 '22 січня 1919', '22 лютого 1919', '1 січня 1919', '1 лютого 1919', 'A'),
('Хто був автором Конституції Пилипа Орлика?', 
 'Пилип Орлик', 'Іван Мазепа', 'Іван Мазепинець', 'Григорій Сковорода', 'A'),
('У якому році відбулася Чорнобильська катастрофа?', 
 '1984', '1986', '1988', '1990', 'B');
GO
