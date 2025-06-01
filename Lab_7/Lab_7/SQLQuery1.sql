USE TransportDbLab7;

CREATE TABLE Transports
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TransportType NVARCHAR(2) NOT NULL,
    RouteNumber NVARCHAR(10) NOT NULL,
    RouteDistance FLOAT NOT NULL,
    TravelTime INT NOT NULL
);

-- Додаткове наповнення
INSERT INTO Transports (TransportType, RouteNumber, RouteDistance, TravelTime)
VALUES
    ('Tr', '12A', 14.5, 30),
    ('Tl', '5b', 8.2, 25),
    ('A',  '101', 20.0, 40);