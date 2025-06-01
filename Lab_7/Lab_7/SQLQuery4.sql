USE TransportDBLab7_3;

CREATE TABLE Transports
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    VidTransportu NVARCHAR(2) NOT NULL,
    NomMarshruta NVARCHAR(20) NOT NULL,
    ProtjazhnistMarshruta REAL NOT NULL, -- щоб відповідати float C#
    ChasVDorozi INT NOT NULL,
    KilkistZupynok INT NOT NULL
);

INSERT INTO Transports (VidTransportu, NomMarshruta, ProtjazhnistMarshruta, ChasVDorozi, KilkistZupynok)
VALUES
('Tr', '12A', 14.5, 30, 5),
('Tl', '5b', 8.2, 25, 3),
('A',  '101', 20.0, 40, 7);