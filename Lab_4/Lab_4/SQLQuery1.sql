CREATE TABLE [dbo].[Employees]
(
    [e_id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [e_name] NVARCHAR(100) NOT NULL,
    [e_salary] DECIMAL(18, 2) NOT NULL,
    [e_age] INT NOT NULL,
    [e_gender] NVARCHAR(10) NOT NULL,
    [e_dept] NVARCHAR(50) NOT NULL
);

-- Приклад вставки кількох записів
INSERT INTO [dbo].[Employees] (e_name, e_salary, e_age, e_gender, e_dept)
VALUES
('Sam', 95000, 45, 'Male', 'Operations'),
('Bob', 80000, 21, 'Male', 'Support'),
('Anne', 125000, 25, 'Female', 'Analytics'),
('Julia', 73000, 30, 'Female', 'Analytics'),
('Matt', 159000, 33, 'Male', 'Sales'),
('Jeff', 112000, 27, 'Male', 'Operations');
