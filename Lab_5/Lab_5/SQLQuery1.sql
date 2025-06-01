USE RegistrationDB;

-- Створення таблиці Accounts для збереження даних реєстрації
CREATE TABLE Accounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Login NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);