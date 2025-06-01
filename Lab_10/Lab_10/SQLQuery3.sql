USE Testet01DB;
GO

-- 1. Перевіримо тип стовпця — він має бути NVARCHAR, а не VARCHAR
SELECT 
    COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = N'Questions' AND COLUMN_NAME = N'CorrectAnswers';
GO

-- Якщо виявиться VARCHAR, перетворимо на NVARCHAR(200)
ALTER TABLE dbo.Questions
ALTER COLUMN CorrectAnswers NVARCHAR(200) NOT NULL;
GO

-- 2. Оновимо значення для текстових питань (з урахуванням точного Text)
UPDATE dbo.Questions
SET CorrectAnswers = N'Київ'
WHERE QuestionType = 'T'
  AND Text = N'Напишіть столицю України';
GO

UPDATE dbo.Questions
SET CorrectAnswers = N'1991'
WHERE QuestionType = 'T'
  AND Text = N'У якому році проголошено незалежність України?';
GO

UPDATE dbo.Questions
SET CorrectAnswers = N'Чубинський'
WHERE QuestionType = 'T'
  AND Text = N'Прізвище автора слів Гімну України?';
GO

-- 3. Перевіримо, що оновлення успішне
SELECT 
    Id,
    Text,
    QuestionType,
    CorrectAnswers
FROM dbo.Questions
WHERE QuestionType = 'T';
GO
