USE RegistrDB;
GO

-- Заповнення таблиці Accounts тестовими даними
INSERT INTO Accounts (Password, Email)
VALUES ('Password123', 'user1@example.com');

INSERT INTO Accounts (Password, Email)
VALUES ('MySecretPass', 'user2@example.com');

INSERT INTO Accounts (Password, Email)
VALUES ('AdminPass!', 'admin@example.com');
GO
