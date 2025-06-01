USE TransportDbLab7_1;

CREATE TABLE Transports
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    VidTransportu NVARCHAR(2) NOT NULL,
    NomMarshruta NVARCHAR(15) NOT NULL,
    ProtjazhnistMarshruta FLOAT NOT NULL,
    ChasVDorozi INT NOT NULL,
    KilkistZupynok INT NOT NULL
);

-- Приклад початкових даних
INSERT INTO Transports 
(VidTransportu, NomMarshruta, ProtjazhnistMarshruta, ChasVDorozi, KilkistZupynok)
VALUES
('Tr', '12A', 14.5, 30, 5),
('Tl', '5b', 8.2, 25, 3),
('A',  '101', 20.0, 40, 7);