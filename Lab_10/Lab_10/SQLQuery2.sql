GO

USE TestetDB;
GO

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

CREATE TABLE TestResults (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Score INT NOT NULL,
    TakenAt DATETIME NOT NULL
);
GO

INSERT INTO Questions (Text, OptionA, OptionB, OptionC, OptionD, CorrectOption) VALUES
(N'Яка дата Першого Збору Української Центральної Ради?', 
 N'17 березня 1917', 
 N'7 березня 1917', 
 N'17 квітня 1917', 
 N'7 квітня 1917', 
 'A'),
(N'Хто очолив УНР у 1918 році як гетьман?', 
 N'Володимир Винниченко', 
 N'Олександр Шульгин', 
 N'Павло Скоропадський', 
 N'Симон Петлюра', 
 'C'),
(N'Коли відбулося проголошення Акта Злуки УНР та ЗУНР?', 
 N'22 січня 1919', 
 N'22 лютого 1919', 
 N'1 січня 1919', 
 N'1 лютого 1919', 
 'A'),
(N'Хто був автором Конституції Пилипа Орлика?', 
 N'Пилип Орлик', 
 N'Іван Мазепа', 
 N'Іван Мазепинець', 
 N'Григорій Сковорода', 
 'A'),
(N'У якому році відбулася Чорнобильська катастрофа?', 
 N'1984', 
 N'1986', 
 N'1988', 
 N'1990', 
 'B');
GO
