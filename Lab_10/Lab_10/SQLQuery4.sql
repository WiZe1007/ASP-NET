USE Testet01DB;
GO

-- Видалити старі таблиці (якщо вони були)
IF OBJECT_ID(N'dbo.TestResults', N'U') IS NOT NULL
    DROP TABLE dbo.TestResults;
IF OBJECT_ID(N'dbo.Questions',   N'U') IS NOT NULL
    DROP TABLE dbo.Questions;
GO

-- Створити таблицю запитань із новими полями QuestionType та CorrectAnswers
CREATE TABLE dbo.Questions
(
    Id              INT             IDENTITY(1,1) PRIMARY KEY,
    Text            NVARCHAR(500)   NOT NULL,
    OptionA         NVARCHAR(200)   NOT NULL,
    OptionB         NVARCHAR(200)   NOT NULL,
    OptionC         NVARCHAR(200)   NOT NULL,
    OptionD         NVARCHAR(200)   NOT NULL,
    QuestionType    CHAR(1)         NOT NULL
                       CONSTRAINT DF_Questions_QuestionType DEFAULT ('R'),
    CorrectAnswers  NVARCHAR(200)   NOT NULL
                       CONSTRAINT DF_Questions_CorrectAnswers DEFAULT ('')
);
GO

-- Створити таблицю результатів
CREATE TABLE dbo.TestResults
(
    Id       INT       IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100)      NOT NULL,
    Score    INT               NOT NULL,
    TakenAt  DATETIME           NOT NULL
);
GO

-- Заповнити таблицю Questions 10 тестовими питаннями
-- 4 питання типу R (radio), 3 питання типу C (checkbox), 3 питання типу T (text)
INSERT INTO dbo.Questions
    (Text, OptionA,              OptionB,                 OptionC,             OptionD,            QuestionType, CorrectAnswers)
VALUES
-- 1–4: радіокнопки (лише одна правильна відповідь)
(N'Яка дата Першого Збору Української Центральної Ради?',
  N'17 березня 1917', N'7 березня 1917', N'17 квітня 1917', N'7 квітня 1917',
  'R', 'A'),
(N'Хто очолив УНР у 1918 році як гетьман?',
  N'Володимир Винниченко', N'Олександр Шульгин', N'Павло Скоропадський', N'Симон Петлюра',
  'R', 'C'),
(N'Коли відбулося проголошення Акта Злуки УНР та ЗУНР?',
  N'22 січня 1919', N'22 лютого 1919', N'1 січня 1919', N'1 лютого 1919',
  'R', 'A'),
(N'У якому році сталася Чорнобильська катастрофа?',
  N'1984', N'1986', N'1988', N'1990',
  'R', 'B'),

-- 5–7: чекбокси (декілька правильних відповідей, зберігаємо безпосередньо символи варіантів)
(N'Які з наведених міст входять до Золотого кільця України?',
  N'Київ', N'Львів', N'Чернігів', N'Чернівці',
  'C', 'ABC'),
(N'Які символи є державними символами України?',
  N'Тризуб', N'Прапор', N'Гімн', N'Калина',
  'C', 'ABC'),
(N'Які з перелічених мов є офіційними мовами ООН?',
  N'Англійська', N'Французька', N'Українська', N'Іспанська',
  'C', 'ABD'),

-- 8–10: текстовий ввід (правильна відповідь — точний текст)
(N'Напишіть столицю України',
  N'', N'', N'', N'', 
  'T', 'Київ'),
(N'У якому році проголошено незалежність України?',
  N'', N'', N'', N'',
  'T', '1991'),
(N'Прізвище автора слів Гімну України?',
  N'', N'', N'', N'',
  'T', 'Чубинський')
;
GO
