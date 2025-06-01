USE RegistrDB;

-- Створення таблиці Accounts для збереження даних акаунту (за варіантом №12)
CREATE TABLE Accounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
