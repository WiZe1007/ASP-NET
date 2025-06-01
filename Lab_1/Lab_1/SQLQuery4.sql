IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ColumnCounts')
BEGIN
    CREATE TABLE [dbo].[ColumnCounts](
        [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        [Columns] INT NOT NULL
    );
END

-- Вставляємо 1 запис, наприклад: Columns=6
INSERT INTO [dbo].[ColumnCounts] ([Columns]) VALUES (6);
